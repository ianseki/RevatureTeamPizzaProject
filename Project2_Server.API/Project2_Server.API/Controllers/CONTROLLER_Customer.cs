using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;




namespace Project2_Server.API.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CONTROLLER_Customer : ControllerBase
    {
        // FIELDS
        private readonly INTERFACE_SQL_Customer API_PROP_INTERFACE_Customer;
        private readonly INTERFACE_SQL_Order API_PROP_INTERFACE_Order;
        private readonly INTERFACE_SQL_Project API_PROP_INTERFACE_Project;
        private readonly INTERFACE_SQL_LinkingTable API_PROP_INTERFACE_LinkingTable;
        private readonly ILogger<CONTROLLER_Customer> API_DATA_Logger;

        // CONSTRUCTORS
        public CONTROLLER_Customer(INTERFACE_SQL_Customer INPUT_INTERFACE_Customer, INTERFACE_SQL_Order INPUT_INTERFACE_Order, INTERFACE_SQL_Project INPUT_INTERFACE_Project, INTERFACE_SQL_LinkingTable INPUT_INTERFACE_LinkingTable, ILogger<CONTROLLER_Customer> INPUT_Logger)
        {
            this.API_PROP_INTERFACE_Customer = INPUT_INTERFACE_Customer;
            this.API_PROP_INTERFACE_Order = INPUT_INTERFACE_Order;
            this.API_PROP_INTERFACE_Project = INPUT_INTERFACE_Project;
            this.API_PROP_INTERFACE_LinkingTable = INPUT_INTERFACE_LinkingTable;
            this.API_DATA_Logger = INPUT_Logger;
        }

        [HttpGet("INPUT_Email/INPUT_Password")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> API_ASYNC_CUSTOMER_checkValidLogin(string INPUT_Email, string INPUT_Password)
        {
            DMODEL_Customer WORK_DMODEL_Customer = await API_PROP_INTERFACE_Customer.CUSTOMER_ASYNC_checkCustomerLogin(INPUT_Email);

            if (WORK_DMODEL_Customer.customer_id == -1)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_checkValidLogin --> OUTPUT: User doesn't exist in database");
                return false;
            }

            if (WORK_DMODEL_Customer.password == INPUT_Password)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_checkValidLogin --> OUTPUT: User login verified");
                return true;
            }
            else
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_checkValidLogin --> OUTPUT: User password is incorrect");
                return false;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<bool> API_ASYNC_CUSTOMER_createNewCustomer(DMODEL_Customer INPUT_DMODEL_Customer)
        {
            try
            {
                await API_PROP_INTERFACE_Customer.CUSTOMER_ASYNC_createNewCustomer(INPUT_DMODEL_Customer);

                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_createNewCustomer --> OUTPUT: Created new customer");
                return true;
            }
            catch (Exception e)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_createNewCustomer --> OUTPUT: Failed to create new customer");
                API_DATA_Logger.LogError(e, e.Message);
                return false;
            }
        }

        [HttpPost("INPUT_CustomerID/INPUT_DMODEL_Order/INPUT_LIST_DMODEL_Projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> API_ASYNC_CUSTOMER_createNewOrder(int INPUT_CustomerID, DMODEL_Order INPUT_DMODEL_Order, List<DMODEL_Project> INPUT_LIST_DMODEL_Projects)
        {
            // Adds Order into [Project2].[Order] Table
            int WORK_generatedOrderID = await API_PROP_INTERFACE_Order.ORDER_ASYNC_createNewOrder(INPUT_DMODEL_Order);

            if (WORK_generatedOrderID == -1)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_createNewOrder --> OUTPUT: Recieved failed signal when creating new order, aborting task");
                return false;
            }

            // Adds All Projects into [Project2].[Project] Table
            List<int> WORK_generatedProjectIDs = new List<int>();

            foreach (DMODEL_Project TEMP_Project in INPUT_LIST_DMODEL_Projects)
            {
                int TEMP_generatedProjectID = await API_PROP_INTERFACE_Project.PROJECT_ASYNC_createNewProject(TEMP_Project);

                if(TEMP_generatedProjectID == -1)
                {
                    API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_CUSTOMER_createNewOrder --> OUTPUT: Recieved failed signal when creating new project for order {0}, aborting task", WORK_generatedOrderID);
                    return false;
                }

                WORK_generatedProjectIDs.Add(TEMP_generatedProjectID);
            }

            // Adds corresponding connection to each linking table
            await API_PROP_INTERFACE_LinkingTable.LINKING_ASYNC_addToCustomerOrderLinkingTable(INPUT_CustomerID, WORK_generatedOrderID);

            foreach (int TEMP_ProjectID in WORK_generatedProjectIDs)
            {
                await API_PROP_INTERFACE_LinkingTable.LINKING_ASYNC_addToOrderProjectLinkingTable(WORK_generatedOrderID, TEMP_ProjectID);
            }

            return true;
        }
    }
}
