using System;

namespace Project02_Server.Data
{
    public class SQL_Employee : INTERFACE_Repository
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly string API_PROP_logger;

        // CONSTRUCTORS
        public SQL_Employee(string INPUT_connectionString, string INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS
    }
}
