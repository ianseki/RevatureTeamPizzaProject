using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_Order : INTERFACE_SQL_Order
    {
        // FIELD
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_Order> API_PROP_logger;

        // CONSTRUCTORS
        public SQL_Order(string INPUT_connectionString, ILogger<SQL_Order> INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
        public async Task<DMODEL_Order> ORDER_ASYNC_getOrderData(int INPUT_OrderID)
        {
            SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT order_id, time_of_order, status FROM [PROJECT2].[Order] WHERE order_id = @INPUT_OrderID";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_OrderID", INPUT_OrderID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();
            
            if (DB_reader.HasRows == false)
            {
                DMODEL_Order OUTPUT_blank = new DMODEL_Order(-1, DateTime.MinValue, false);

                API_PROP_logger.LogInformation("EXECUTED: ORDER_ASYNC_getOrderData --> OUTPUT: Can't find Order {0}, returning blank order", INPUT_OrderID);
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_OrderID = DB_reader.GetInt32(0);
                DateTime WORK_DateTime = DB_reader.GetDateTime(1);
                bool WORK_Status = DB_reader.GetBoolean(2);

                DMODEL_Order OUTPUT_Order = new DMODEL_Order(WORK_OrderID, WORK_DateTime, WORK_Status);

                API_PROP_logger.LogInformation("EXECUTED: ORDER_ASYNC_getOrderData --> OUTPUT: Returning Order {0} information", INPUT_OrderID);
                await DB_connection.CloseAsync();
                return OUTPUT_Order;
            }
        }


       

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ORDER_ASYNC_createNewOrder(DMODEL_Order INPUT_DMODEL_Order)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "INSERT INTO [PROJECT2].[Order] (time_of_order, status) VALUES (@INPUT_TimeOfOrder, INPUT_Status);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_TimeOfOrder", INPUT_DMODEL_Order.time_of_order);
                DB_command.Parameters.AddWithValue("@INPUT_Status", INPUT_DMODEL_Order.status);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: ORDER_ASYNC_createNewOrder --> OUTPUT: Succesfully created new order");
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: ORDER_ASYNC_createNewOrder --- RETURNED: FAILED to create order");
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> ORDER_ASYNC_changeOrderStatus(int INPUT_OrderID, bool INPUT_Status)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "UPDATE [PROJECT2].[Order] SET status = @INPUT_Status WHERE order_id = @INPUT_OrderID;";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_OrderID", INPUT_OrderID);
                DB_command.Parameters.AddWithValue("@INPUT_Status", INPUT_Status);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: ORDER_ASYNC_changeOrderStatus --> OUTPUT: Succesfully changed order {0} status to {1}", INPUT_OrderID, INPUT_Status);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: ORDER_ASYNC_changeOrderStatus --- OUTPUT: FAILED to change order {0} status", INPUT_OrderID);
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }
    }
}
