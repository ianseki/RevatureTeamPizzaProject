using Microsoft.Extensions.Logging;
using Project2_Server.Model;
using System;
using System.Data.SqlClient;

namespace Project2_Server.Data
{
    public class SQL_Project : INTERFACE_SQL_Project
    {
        // FIELD
        private readonly string DB_PROP_connectionString;
        private readonly ILogger<SQL_Project> API_PROP_logger;

        // CONSTRUCTORS
        public SQL_Project(string INPUT_connectionString, ILogger<SQL_Project> INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
        public async Task<DMODEL_Project> PROJECT_ASYNC_getProjectData(int INPUT_ProjectID)
        {
            SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
            await DB_connection.OpenAsync();

            string DB_commandText = @"SELECT project_id, item_id, completion_status FROM [PROJECT2].[Project] WHERE project_id = @INPUT_ProjectID";

            using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
            DB_command.Parameters.AddWithValue("@INPUT_ProjectID", INPUT_ProjectID);

            using SqlDataReader DB_reader = await DB_command.ExecuteReaderAsync();

            if (DB_reader.HasRows == false)
            {
                DMODEL_Project OUTPUT_blank = new DMODEL_Project(-1, -1, false);

                API_PROP_logger.LogInformation("EXECUTED: PROJECT_ASYNC_getProjectData --> OUTPUT: Can't find Project {0}, returning blank order", INPUT_ProjectID);
                await DB_connection.CloseAsync();
                return OUTPUT_blank;
            }
            else
            {
                await DB_reader.ReadAsync();

                int WORK_ProjectID = DB_reader.GetInt32(0);
                int WORK_ItemID = DB_reader.GetInt32(1);
                bool WORK_Status = DB_reader.GetBoolean(2);

                DMODEL_Project OUTPUT_Project = new DMODEL_Project(WORK_ProjectID, WORK_ItemID, WORK_Status);

                API_PROP_logger.LogInformation("EXECUTED: PROJECT_ASYNC_getProjectData --> OUTPUT: Returning Project {0} information", INPUT_ProjectID);
                await DB_connection.CloseAsync();
                return OUTPUT_Project;
            }
        }

        public async Task<bool> PROJECT_ASYNC_createNewProject(DMODEL_Project INPUT_DMODEL_Project)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "INSERT INTO [PROJECT2].[Project] (item_id, completion_status) VALUES (@INPUT_ItemID, INPUT_Status);";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_ItemID", INPUT_DMODEL_Project.item_id);
                DB_command.Parameters.AddWithValue("@INPUT_Status", INPUT_DMODEL_Project.completion_status);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: PROJECT_ASYNC_createNewProject --> OUTPUT: Succesfully created new project");
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: PROJECT_ASYNC_createNewProject --- RETURNED: FAILED to create project");
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }

        public async Task<bool> PROJECT_ASYNC_changeProjectStatus(int INPUT_ProjectID, bool INPUT_Status)
        {
            try
            {
                SqlConnection DB_connection = new SqlConnection(DB_PROP_connectionString);
                await DB_connection.OpenAsync();

                string DB_commandText = "UPDATE [PROJECT2].[Project] SET completion_status = @INPUT_Status WHERE project_id = @INPUT_ProjectID;";

                using SqlCommand DB_command = new SqlCommand(DB_commandText, DB_connection);
                DB_command.Parameters.AddWithValue("@INPUT_ProjectID", INPUT_ProjectID);
                DB_command.Parameters.AddWithValue("@INPUT_Status", INPUT_Status);

                await DB_command.ExecuteNonQueryAsync();

                API_PROP_logger.LogInformation("EXECUTED: PROJECT_ASYNC_changeProjectStatus --> OUTPUT: Succesfully changed project {0} status to {1}", INPUT_ProjectID, INPUT_Status);
                await DB_connection.CloseAsync();
                return true;
            }
            catch (Exception e)
            {
                API_PROP_logger.LogError("EXECUTED: PROJECT_ASYNC_changeProjectStatus --- OUTPUT: FAILED to change project {0} status", INPUT_ProjectID);
                API_PROP_logger.LogError(e, e.Message);
                return false;
            }
        }
    }
}
