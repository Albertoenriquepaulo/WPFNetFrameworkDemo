using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WPFNetFrameworkDemo.Models
{
    public class EmployeeService
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        public EmployeeService()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnection"].ConnectionString);
            sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure
            };
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "udp_SelectAllEmployees";
                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    Employee employee = null;
                    while (sqlDataReader.Read())
                    {
                        employee = new Employee
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.GetString(1),
                            Age = sqlDataReader.GetInt32(2)
                        };

                        employees.Add(employee);
                    }

                }
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

            return employees;
        }

        public bool Add(Employee employee)
        {
            bool isAdded = false;

            if (employee?.Age >= 18 && employee.Age <= 51)
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.CommandText = "udp_InsertEmployee";
                    sqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                    sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                    sqlCommand.Parameters.AddWithValue("@Age", employee.Age);

                    sqlConnection.Open();
                    int numberOfRowsAffected = sqlCommand.ExecuteNonQuery();
                    isAdded = numberOfRowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return isAdded;
        }

        public bool Update(Employee employee)
        {
            bool isUpdated = false;
            if (employee == null)
            {
                throw new ArgumentException("The employee must not be null");
            }

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "udp_UpdateEmployee";
                sqlCommand.Parameters.AddWithValue("@Id", employee.Id);
                sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("@Age", employee.Age);

                sqlConnection.Open();
                int numberOfRowsAffected = sqlCommand.ExecuteNonQuery();
                isUpdated = numberOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;

            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "udp_DeleteEmployee";
                sqlCommand.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                int numberOfRowsAffected = sqlCommand.ExecuteNonQuery();
                isDeleted = numberOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

            return isDeleted;
        }

        public Employee Search(int id)
        {
            Employee employee = null;
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "udp_SelectEmployeeById";
                sqlCommand.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    employee = new Employee
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Name = sqlDataReader.GetString(1),
                        Age = sqlDataReader.GetInt32(2)
                    };
                }
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

            return employee;
        }

    }
}
