using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace ModelBureauet.Models
{
    public class Model
    {
        public Model(string name, int phoneNumber, string address, int height, int weight, string hairColour, string comment)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Height = height;
            Weight = weight;
            HairColour = hairColour;
            Comment = comment;
        }

        public int modelId { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string HairColour { get; set; }
        public string Comment { get; set; }
        public IncomingTask incomingTask { get; set; }
    }

    public class Models : ObservableCollection<Model>
    {
        private DataAccessLayer dal = new DataAccessLayer();

        public Models()
        {
            getAllModels();
        }

        private async void getAllModels()
        {

            var products = await dal.GetModelsInDb();

            foreach (var product in products)
            {
                if (!Contains(product))
                {
                    Add(product);
                }
            }

        }

        private Model currentModel;
        public Model CurrentModel
        {
            get
            {


                return currentModel;
            }
            set
            {
                if (currentModel != value)
                {
                    currentModel = value;
                    NotifyPropertyChanged("CurrentModel");
                }
            }
        }

        public int SelectedIndex { get; set; }


        //ICommand _assignCommand;
        //public ICommand AssignCommand
        //{
        //    get { return _assignCommand ?? (_assignCommand = new RelayCommand<object>(AssignWindow)); }
        //}

        //private void AssignWindow(object parameter)
        //{

        //}


        ICommand _addModelCommand;
        public ICommand AddModelCommand
        {
            get { return _addModelCommand ?? (_addModelCommand = new RelayCommand<object>(AddModel)); }
        }

        private async void AddModel(object parameter)
        {
            var values = (object[])parameter;
            var name = values[0].ToString();
            var phoneNumber = values[1].ToString();
            var address = (values[2].ToString());
            var height = (values[3].ToString());
            var weight= (values[4].ToString());
            var hairColour= (values[5].ToString());
            var comment= (values[6].ToString());

            Model model = new Model(name, int.Parse(phoneNumber), address, int.Parse(height), int.Parse(weight)
            , hairColour, comment);

            var response = await dal.CreateModelInDb(model);
            if (response)
            {
                MessageBox.Show("You successfully created a model in the database");
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
