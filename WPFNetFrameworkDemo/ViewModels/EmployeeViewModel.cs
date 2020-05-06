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
            UpdateCommand = new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);
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
                bool IsSaved = ObjEmployeeService.Add(Clone(currentEmployee));
                if (IsSaved)
                {
                    //CurrentEmployee.Id = 0;
                    //CurrentEmployee.Name = "";
                    //CurrentEmployee.Age = 0;
                    Message = "Employee Saved";
                    LoadData();
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

        #region SearchOperation
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
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
            set { updateCommand = value; }
        }

        public void Update()
        {
            try
            {
                bool IsUpdated = ObjEmployeeService.Update(Clone(CurrentEmployee));
                if (IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update Operation Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        public void Delete()
        {
            try
            {

                bool IsUpdated = ObjEmployeeService.Delete((Clone(currentEmployee)).Id);
                if (IsUpdated)
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete Operation Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion


        public Employee Clone(Employee obj)
        {
            Employee clone = (Employee)obj.Clone();
            clone.Id = CurrentEmployee.Id;
            clone.Name = CurrentEmployee.Name;
            clone.Age = CurrentEmployee.Age;
            return clone;
        }
    }
}
