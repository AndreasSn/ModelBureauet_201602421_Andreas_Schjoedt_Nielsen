using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModelBureauet.Models
{
    public class IncomingTask
    {
        public IncomingTask(string costumer, DateTime startDate, int numberOfDays, string location, int numberOfModels, string comments)
        {
            Costumer = costumer;
            StartDate = startDate;
            NumberOfDays = numberOfDays;
            Location = location;
            NumberOfModels = numberOfModels;
            Comments = comments;
        }

        public long IncomingTaskId { get; set; }
        public string Costumer { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Location{ get; set; }
        public int NumberOfModels{ get; set; }
        public string Comments{ get; set; }
    }

    public class IncomingTasks : ObservableCollection<IncomingTask>
    {
        private DataAccessLayer dal = new DataAccessLayer();

        public IncomingTasks()
        {
            getAllTasks();
        }

        private async void getAllTasks()
        {

            var products = await dal.GetTasksInDb();

            foreach (var product in products)
            {
                if (!Contains(product))
                {
                    Add(product);
                }
            }

        }

        private IncomingTask currentTask;
        public IncomingTask CurrentTask
        {
            get
            {
                if (currentTask == null)
                {
                    return null;
                }

                return currentTask;
            }
            set
            {
                if (currentTask != value)
                {
                    currentTask = value;
                    NotifyPropertyChanged("CurrentTask");
                }
            }
        }


        ObservableCollection<Model> assignedModels = new ObservableCollection<Model>();

        public ObservableCollection<Model> AssignedModels
        {
            get { return assignedModels; }
            set { assignedModels = value; NotifyPropertyChanged("AssignedModels"); }
        }

        ICommand _assignInDatabaseCommand;
        public ICommand AssignInDatabaseCommand
        {
            get { return _assignInDatabaseCommand ?? (_assignInDatabaseCommand = new RelayCommand<object>(AssignToDatabase)); }
        }
        private async void AssignToDatabase(object parameter)
        {
            var result = (object[])parameter;

            var model = (Model)result[0];
            var taskId = int.Parse(result[1].ToString());

            ModeltaskDTO modeltask = new ModeltaskDTO();
            modeltask.modelId = model.modelId;
            modeltask.incomingTaskId = taskId;

            var response = await dal.CreateModelTaskInDb(modeltask);
            if (response)
            {
                AssignedModels.Add(model);
                MessageBox.Show("Succesfully assignet the model to the task");
            }
            else MessageBox.Show("Something went wrong");

        }

        ICommand _assignCommand;
        public ICommand AssignCommand
        {
            get { return _assignCommand ?? (_assignCommand = new RelayCommand<object>(Assign)); }
        }

        private void Assign(object parameter)
        {
            var model = (Model) parameter;
            AssignedModels.Add(model);
        }

        ICommand _addTaskCommand;
        public ICommand AddTaskCommand
        {
            get { return _addTaskCommand ?? (_addTaskCommand = new RelayCommand<object>(AddTask)); }
        }

        private async void AddTask(object parameter)
        {
            var values = (object[])parameter;
            var costumer = values[0].ToString();
            var startDate = values[1].ToString();
            var numberOfDays = (values[2].ToString());
            var location = (values[3].ToString());
            var numberOfModels= (values[4].ToString());
            var comment = (values[5].ToString());

            IncomingTask incomingTask = new IncomingTask(costumer, DateTime.Parse(startDate), int.Parse(numberOfDays), location, int.Parse(numberOfModels)
                , comment);

            var response = await dal.CreateTaskInDb(incomingTask);
            if (response)
            {
                MessageBox.Show("You successfully created a task in the database");
            }
            else MessageBox.Show("Something went wrong");
        }





        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
