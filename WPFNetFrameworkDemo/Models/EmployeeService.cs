using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFNetFrameworkDemo.Models
{
    public class EmployeeService
    {
        private static List<Employee> ObjEmployeeList;

        public EmployeeService()
        {
            ObjEmployeeList = new List<Employee>()
            {
                new Employee{ Id=101, Name = "Alberto", Age = 41 }
            };
        }

        public List<Employee> GetAll()
        {
            return ObjEmployeeList;
        }

        public bool Add(Employee employee)
        {
            if (employee?.Age >= 18 && employee.Age <= 51)
            {
                ObjEmployeeList.Add(employee);
                return true;
            }
            throw new ArgumentException("Invalid age limit");
        }

        public bool Update(Employee employee)
        {
            bool isUpdated = false;
            if (employee == null)
            {
                throw new ArgumentException("The employee must not be null");
            }
            ObjEmployeeList.ForEach(e =>
            {
                if (e.Id == employee.Id)
                {
                    e.Id = employee.Id;
                    e.Name = employee.Name;
                    e.Age = employee.Age;
                    isUpdated = true;
                    return;
                }
            });
            return isUpdated;
        }

        public bool Delete(int id)
        {
            Employee employee = ObjEmployeeList.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return false;
            }
            ObjEmployeeList.Remove(employee);
            return true;
        }

        public Employee Search(int id)
        {
            return ObjEmployeeList.FirstOrDefault(x => x.Id == id);
        }

    }
}
