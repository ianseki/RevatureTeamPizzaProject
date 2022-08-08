using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_Employee : INTERFACE_SQL_Employee
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_Employee> API_PROP_logger;

        // CONSTRUCTORS
        public SQL_Employee(string INPUT_connectionString, ILogger<SQL_Employee> INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
        public async Task<DMODEL_Employee> EMPLOYEE_ASYNC_getEmployeeData(int INPUT_EmployeeID)
        {
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT employee_id, first_name, last_name, email, password FROM [PROJECT2].[Employee] WHERE employee_id = @INPUT_EmployeeID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_EmployeeID", INPUT_EmployeeID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                DMODEL_Employee OUTPUT_blank = new DMODEL_Employee(-1, "", "", "", "");

                API_PROP_logger.LogInformation("EXECUTED: EMPLOYEE_ASYNC_getEmployeeData --- OUTPUT: Returns blank user");
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_EmployeeID = DB_reader.GetInt32(0);
                string WORK_Firstnanme = DB_reader.GetString(1);
                string WORK_Lastname = DB_reader.GetString(2);
                string WORK_Email = DB_reader.GetString(3);
                string WORK_Password = DB_reader.GetString(4);

                DMODEL_Employee OUTPUT_Employee = new DMODEL_Employee(WORK_EmployeeID, WORK_Firstnanme, WORK_Lastname, WORK_Email, WORK_Password);

                API_PROP_logger.LogInformation("EXECUTED: EMPLOYEE_ASYNC_getEmployeeData --- OUTPUT: Returns employee {0} data", WORK_Email);
                await DB_connection.CloseAsync();
                return OUTPUT_Employee;
            }
        }

        public async Task<DMODEL_Employee> EMPLOYEE_ASYNC_checkEmployeeLogin(string INPUT_Email)
        {
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT employee_id, first_name, last_name, email, password FROM [PROJECT2].[Employee] WHERE email = @INPUT_Email;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_Email", INPUT_Email);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                DMODEL_Employee OUTPUT_blank = new DMODEL_Employee(-1, "", "", "", "");

                API_PROP_logger.LogInformation("EXECUTED: EMPLOYEE_ASYNC_checkEmployeeLogin --- OUTPUT: Returns blank user");
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_EmployeeID = DB_reader.GetInt32(0);
                string WORK_Firstnanme = DB_reader.GetString(1);
                string WORK_Lastname = DB_reader.GetString(2);
                string WORK_Email = DB_reader.GetString(3);
                string WORK_Password = DB_reader.GetString(4);

                DMODEL_Employee OUTPUT_Employee = new DMODEL_Employee(WORK_EmployeeID, WORK_Firstnanme, WORK_Lastname, WORK_Email, WORK_Password);

                API_PROP_logger.LogInformation("EXECUTED: EMPLOYEE_ASYNC_checkEmployeeLogin --- OUTPUT: Returned login passed for {0}", WORK_Email);
                await DB_connection.CloseAsync();
                return OUTPUT_Employee;
            }

        }

        public async Task<bool> EMPLOYEE_ASYNC_createNewEmployee(DMODEL_Employee INPUT_DMODEL_Employee)
        {
            try
            {
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"INSERT INTO [PROJECT2].[Employee] (first_name, last_name, email, password) VALUES ( @INPUT_Firstname, @INPUT_Lastname, @INPUT_Email, @INPUT_Password);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_Firstname", INPUT_DMODEL_Employee.first_name);
                DB_command.Parameters.AddWithValue("@INPUT_Lastname", INPUT_DMODEL_Employee.last_name);
                DB_command.Parameters.AddWithValue("@INPUT_Email", INPUT_DMODEL_Employee.email);
                DB_command.Parameters.AddWithValue("@INPUT_Password", INPUT_DMODEL_Employee.password);

                await DB_command.ExecuteNonQueryAsync();


                API_PROP_logger.LogInformation("EXECUTED: EMPLOYEE_ASYNC_createNewEmployee --- OUTPUT: Created customer {0}", INPUT_DMODEL_Employee.email);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: EMPLOYEE_ASYNC_createNewEmployee --- OUTPUT: FAILED to create user {0}", INPUT_DMODEL_Employee.email);
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }
    }
}
