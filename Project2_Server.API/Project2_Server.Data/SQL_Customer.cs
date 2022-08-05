using System;

namespace Project2_Server.Data
{
    public class SQL_Customer : INTERFACE_Repository
    {
        // FIELDS
        private readonly string DB_PROP_connectionString;
        private readonly string API_PROP_logger;
        // CONSTRUCTORS
        public SQL_Customer(string INPUT_connectionString, string INPUT_logger)
        {
            this.DB_PROP_connectionString = INPUT_connectionString;
            this.API_PROP_logger = INPUT_logger;
        }

        // METHODS

    }
}
