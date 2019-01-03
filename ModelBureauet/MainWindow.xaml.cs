using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModelBureauet.Dialogs;
using ModelBureauet.Models;

namespace ModelBureauet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        
        private void BtnAddModel_Click(object sender, RoutedEventArgs e)
        {
            AddModelDialog addModelDialog = new AddModelDialog();
            addModelDialog.Owner = this;
            addModelDialog.DataContext = DataContext;
            if (addModelDialog.ShowDialog() == true)
            {

            }
        }

        private void BtnAddTask_OnClick(object sender, RoutedEventArgs e)
        {
            AddTask addTaskDialog = new AddTask();
            addTaskDialog.Owner = this;
            addTaskDialog.DataContext = DataContext;
            if (addTaskDialog.ShowDialog() == true)
            {

            }
        }

        private void BtnAssignToTask_OnClick(object sender, RoutedEventArgs e)
        {
            AssignTaskDialog assignTaskDialog = new AssignTaskDialog();
            assignTaskDialog.Owner = this;
            assignTaskDialog.DataContext = DataContext;
            if (assignTaskDialog.ShowDialog() == true)
            {

            }
        }
    }
}
