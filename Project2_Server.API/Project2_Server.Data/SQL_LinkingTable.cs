using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_LinkingTable : INTERFACE_SQL_LinkingTable
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_LinkingTable> API_PROP_logger;

        // CONSTRUCTORS
        public SQL_LinkingTable(string INPUT_connectionString, ILogger<SQL_LinkingTable> INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
        public async Task<bool> LINKING_ASYNC_addToCustomerOrderLinkingTable(int INPUT_CustomerID, int INPUT_OrderID)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "INSERT INTO [PROJECT2].[Customer_Order_Link] (customer_id, order_id) VALUES (@INPUT_CustomerID, @INPUT_OrderID);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_CustomerID", INPUT_CustomerID);
                DB_command.Parameters.AddWithValue("@INPUT_OrderID", INPUT_OrderID);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_addToCustomerOrderLinkingTable --> OUTPUT: Succesfully added to linking table customer {0} with order {1}", INPUT_CustomerID, INPUT_OrderID);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: LINKING_ASYNC_addToCustomerOrderLinkingTable --- RETURNED: FAILED to add to linking table");
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<List<int>> LINKING_ASYNC_getFromCustomerOrderLinkingTable(int INPUT_CustomerID)
        {
            SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = "SELECT order_id FROM [PROJECT2].[Customer_Order_Link] WHERE customer_id = @INPUT_CustomerID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_CustomerID", INPUT_CustomerID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                List<int> OUTPUT_blank = new List<int>() { -1 };

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromCustomerOrderLinkingTable --> OUTPUT: FAILED to find orders with customer {0}", INPUT_CustomerID);
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                List<int> OUTPUT_Orders = new List<int>();
                while (await DB_reader.ReadAsync())
                {
                    int WORK_OrderID = DB_reader.GetInt32(0);
                    OUTPUT_Orders.Add(WORK_OrderID);
                }

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromCustomerOrderLinkingTable --> OUTPUT: Successfully got all orderID for customer {0}", INPUT_CustomerID);
                await DB_connection.CloseAsync();
                return OUTPUT_Orders;
            }
        }

        public async Task<bool> LINKING_ASYNC_addToOrderProjectLinkingTable(int INPUT_OrderID, int INPUT_ProjectID)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "INSERT INTO [PROJECT2].[Order_Project_Link] (order_id, project_id) VALUES (@INPUT_OrderID, @INPUT_ProjectID);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_OrderID", INPUT_OrderID);
                DB_command.Parameters.AddWithValue("@INPUT_ProjectID", INPUT_ProjectID);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_addToOrderProjectLinkingTable --> OUTPUT: Succesfully added to linking table order {0} with project {1}", INPUT_OrderID, INPUT_ProjectID);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: LINKING_ASYNC_addToOrderProjectLinkingTable --- RETURNED: FAILED to add to linking table");
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<List<int>> LINKING_ASYNC_getFromOrderProjectLinkingTable(int INPUT_OrderID)
        {
            SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = "SELECT project_id FROM [PROJECT2].[Order_Project_Link] WHERE order_id = @INPUT_OrderID;";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_OrderID", INPUT_OrderID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                List<int> OUTPUT_blank = new List<int>() { -1 };

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromOrderProjectLinkingTable --> OUTPUT: FAILED to find projects with order {0}", INPUT_OrderID);
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                List<int> OUTPUT_Projects = new List<int>();
                while (await DB_reader.ReadAsync())
                {
                    int WORK_ProjectID = DB_reader.GetInt32(0);
                    OUTPUT_Projects.Add(WORK_ProjectID);
                }

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromOrderProjectLinkingTable --> OUTPUT: Successfully got all projectIDs for order {0}", INPUT_OrderID);
                await DB_connection.CloseAsync();
                return OUTPUT_Projects;
            }
        }

        public async Task<List<int>> LINKING_ASYNC_getFromProjectEmployeeLinkingTable(int INPUT_EmployeeID)
        {
            SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT [PROJECT2].[Project_Employee_Link].[project_id]
                                    FROM[PROJECT2].[Project_Employee_Link]
                                    JOIN[PROJECT2].[Project] ON[PROJECT2].[Project_Employee_Link].[project_id] = [PROJECT2].[Project].[project_id]
                                    WHERE[PROJECT2].[Project].[completion_status] = 0 AND[PROJECT2].[Project_Employee_Link].[employee_id] = @INPUT_EmployeeID; ";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_EmployeeID", INPUT_EmployeeID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                List<int> OUTPUT_blank = new List<int>() { -1 };

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromProjectEmployeeLinkingTable --> OUTPUT: FAILED to find projects with employee {0}", INPUT_EmployeeID);
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                List<int> OUTPUT_LIST_ProjectIDs = new List<int>();
                while (await DB_reader.ReadAsync())
                {
                    int WORK_ProjectID = DB_reader.GetInt32(0);
                    OUTPUT_LIST_ProjectIDs.Add(WORK_ProjectID);
                }

                API_PROP_logger.LogInformation("EXECUTED: LINKING_ASYNC_getFromProjectEmployeeLinkingTable --> OUTPUT: Successfully got all projectIDs for employee {0}", INPUT_EmployeeID);
                await DB_connection.CloseAsync();
                return OUTPUT_LIST_ProjectIDs;
            }
        }

    }
}
