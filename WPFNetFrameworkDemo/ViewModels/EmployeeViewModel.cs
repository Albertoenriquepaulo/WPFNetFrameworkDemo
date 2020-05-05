using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using WPFNetFrameworkDemo.Models;
using WPFNetFrameworkDemo.Commands;
using System.Collections.ObjectModel;
namespace WPFNetFrameworkDemo.ViewModels
{
    public class EmployeeViewModel : BaseNotifyPropertyChanged
    {
        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            SaveCommand = new RelayCommand(Save);
            SearchCommand = new RelayCommand(Search);
        }

        #region DisplayOperation
        private ObservableCollection<Employee> employeeList;
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; OnPropertyChanged("EmployeeList"); }
        }
        private void LoadData()
        {
            EmployeeList = new ObservableCollection<Employee>(ObjEmployeeService.GetAll());
        }
        #endregion

        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region SaveOperation
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }
        public void Save()
        {
            try
            {
                bool IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                {
                    Message = "Employee Saved";
                }
                else
                {
                    Message = "Save operation failed";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion

        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
            set { searchCommand = value; }
        }

        public void Search()
        {
            try
            {
                Employee objEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if (objEmployee != null)
                {
                    CurrentEmployee.Name = objEmployee.Name;
                    CurrentEmployee.Age = objEmployee.Age;
                    Message = "Employee Found";
                }
                else
                {
                    Message = "Employee Not Found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }


    }
}
