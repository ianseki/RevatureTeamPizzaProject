using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_Customer : INTERFACE_Repository
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_Customer> API_PROP_logger;
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

            string DB_commandText = "";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_CustomerID", INPUT_CustomerID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                API_PROP_logger.LogInformation("EXECUTED - CUSTOMER_ASYNC_getCustomerData --- RETURNED blank user");

                DMODEL_Customer OUTPUT_blank = new DMODEL_Customer(-1, "", "", "", "");
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

                return OUTPUT_Customer;
            }
        }

    }
}
