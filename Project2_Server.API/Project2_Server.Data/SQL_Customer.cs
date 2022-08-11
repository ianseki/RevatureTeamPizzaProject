using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_Customer : INTERFACE_SQL_Customer
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_Customer> API_PROP_logger;
        //public readonly object Customers; // added for SQL_CustomersControllers

        // CONSTRUCTORS
        public SQL_Customer(string INPUT_connectionString, ILogger<SQL_Customer> INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
        public async Task<DMODEL_Customer> CUSTOMER_ASYNC_getCustomerData(int INPUT_CustomerID)
        {
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT customer_id, first_name, last_name, email, password FROM [PROJECT2].[Customer] WHERE customer_id = @INPUT_CustomerID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_CustomerID", INPUT_CustomerID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                DMODEL_Customer OUTPUT_blank = new DMODEL_Customer(-1, "", "", "", "");

                API_PROP_logger.LogInformation("EXECUTED: CUSTOMER_ASYNC_getCustomerData --- OUTPUT: Returns blank user");
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_CustomerID = DB_reader.GetInt32(0);
                string WORK_Firstnanme = DB_reader.GetString(1);
                string WORK_Lastname = DB_reader.GetString(2);
                string WORK_Email = DB_reader.GetString(3);
                string WORK_Password = DB_reader.GetString(4);

                DMODEL_Customer OUTPUT_Customer = new DMODEL_Customer(WORK_CustomerID, WORK_Firstnanme, WORK_Lastname, WORK_Email, WORK_Password);

                API_PROP_logger.LogInformation("EXECUTED: CUSTOMER_ASYNC_getCustomerData --- OUTPUT: Returns customer {0}", WORK_Email);
                await DB_connection.CloseAsync();
                return OUTPUT_Customer;
            }
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DMODEL_Customer> CUSTOMER_ASYNC_checkCustomerLogin(string INPUT_Email)
        {
            using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT customer_id, first_name, last_name, email, password FROM [PROJECT2].[Customer] WHERE email = @INPUT_Email;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_Email", INPUT_Email);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                DMODEL_Customer OUTPUT_blank = new DMODEL_Customer(-1, "", "", "", "");

                API_PROP_logger.LogInformation("EXECUTED: CUSTOMER_ASYNC_checkCustomerLogin --- OUTPUT: Returns blank user");
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_CustomerID = DB_reader.GetInt32(0);
                string WORK_Firstnanme = DB_reader.GetString(1);
                string WORK_Lastname = DB_reader.GetString(2);
                string WORK_Email = DB_reader.GetString(3);
                string WORK_Password = DB_reader.GetString(4);

                DMODEL_Customer OUTPUT_Customer = new DMODEL_Customer(WORK_CustomerID, WORK_Firstnanme, WORK_Lastname, WORK_Email, WORK_Password);

                API_PROP_logger.LogInformation("EXECUTED: CUSTOMER_ASYNC_checkCustomerLogin --- OUTPUT: Returns data for {0}", WORK_Email);
                await DB_connection.CloseAsync();
                return OUTPUT_Customer;
            }
         
        }

        public async Task<bool> CUSTOMER_ASYNC_createNewCustomer(DMODEL_Customer INPUT_DMODEL_Customer)
        {
            try
            {
                using SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = @"INSERT INTO [PROJECT2].[Customer] (first_name, last_name, email, password) VALUES ( @INPUT_Firstname, @INPUT_Lastname, @INPUT_Email, @INPUT_Password);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_Firstname", INPUT_DMODEL_Customer.first_name);
                DB_command.Parameters.AddWithValue("@INPUT_Lastname", INPUT_DMODEL_Customer.last_name);
                DB_command.Parameters.AddWithValue("@INPUT_Email", INPUT_DMODEL_Customer.email);
                DB_command.Parameters.AddWithValue("@INPUT_Password", INPUT_DMODEL_Customer.password);

                await DB_command.ExecuteNonQueryAsync();


                API_PROP_logger.LogInformation("EXECUTED: CUSTOMER_ASYNC_createNewCustomer --- OUTPUT: Created customer {0}", INPUT_DMODEL_Customer.email);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: CUSTOMER_ASYNC_createNewCustomer --- OUTPUT: FAILED to create user {0}", INPUT_DMODEL_Customer.email);
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }
    }
}
