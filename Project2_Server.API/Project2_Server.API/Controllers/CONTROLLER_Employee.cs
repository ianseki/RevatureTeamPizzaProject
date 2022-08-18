using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;




namespace Project2_Server.API.Controllers
{
    [Route("API/Employee")]
    [ApiController]
    public class CONTROLLER_Employee : ControllerBase
    {
        // FIELDS
        private readonly INTERFACE_SQL_Employee API_PROP_INTERFACE_Employee;
        private readonly INTERFACE_SQL_Order API_PROP_INTERFACE_Order;
        private readonly INTERFACE_SQL_Project API_PROP_INTERFACE_Project;
        private readonly INTERFACE_SQL_LinkingTable API_PROP_INTERFACE_LinkingTable;
        private readonly ILogger<CONTROLLER_Customer> API_DATA_Logger;

        // CONSTRUCTORS
        public CONTROLLER_Employee(INTERFACE_SQL_Employee INPUT_INTERFACE_Employee, INTERFACE_SQL_Order INPUT_INTERFACE_Order, INTERFACE_SQL_Project INPUT_INTERFACE_Project, INTERFACE_SQL_LinkingTable INPUT_INTERFACE_LinkingTable, ILogger<CONTROLLER_Customer> INPUT_Logger)
        {
            this.API_PROP_INTERFACE_Employee = INPUT_INTERFACE_Employee;
            this.API_PROP_INTERFACE_Order = INPUT_INTERFACE_Order;
            this.API_PROP_INTERFACE_Project = INPUT_INTERFACE_Project;
            this.API_PROP_INTERFACE_LinkingTable = INPUT_INTERFACE_LinkingTable;
            this.API_DATA_Logger = INPUT_Logger;
        }

        [HttpGet]
        [Route("CheckLogin")]
        public async Task<int> API_ASYNC_EMPLOYEE_checkValidLogin(string INPUT_Email, string INPUT_Password)
        {
            // Data Verification
            if (INPUT_Email == null || INPUT_Email == "" || INPUT_Password == null || INPUT_Password == "")
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_checkValidLogin --> OUTPUT: Invalid inputs");
                return -1;
            }

            // Logic Implementation
            DMODEL_Employee WORK_DMODEL_Employee = await API_PROP_INTERFACE_Employee.EMPLOYEE_ASYNC_checkEmployeeLogin(INPUT_Email);

            if (WORK_DMODEL_Employee.employee_id == -1)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_checkValidLogin --> OUTPUT: User doesn't exist in database");
                return -1;
            }

            if (WORK_DMODEL_Employee.password == INPUT_Password)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_checkValidLogin --> OUTPUT: User login verified");
                return WORK_DMODEL_Employee.employee_id;
            }
            else
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_checkValidLogin --> OUTPUT: User password is incorrect");
                return -1;
            }
        }

        [HttpPost]
        [Route("CreateNewEmployee")]
        public async Task<bool> API_ASYNC_EMPLOYEE_createNewEmployee([FromBody]DMODEL_Employee INPUT_DMODEL_Employee)
        {
            try
            {
                // Data Verficiation
                INPUT_DMODEL_Employee.DMODEL_EMPLOYEE_verifyData();

                // Logic Implementation
                await API_PROP_INTERFACE_Employee.EMPLOYEE_ASYNC_createNewEmployee(INPUT_DMODEL_Employee);

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

        [HttpPost]
        [Route("UpdateProjectStatus")]
        public async Task<bool> API_ASYNC_EMPLOYEE_updateProjectStatus(int INPUT_ProjectID, bool INPUT_ProjectStatus)
        {
            try
            {
                // Data Verification
                if (INPUT_ProjectID == null || INPUT_ProjectID < 0 || INPUT_ProjectStatus == null)
                {
                    API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_updateProjectStatus --> OUTPUT: Invalid inputs");
                    return false;
                }

                // Logic Implementation
                await API_PROP_INTERFACE_Project.PROJECT_ASYNC_changeProjectStatus(INPUT_ProjectID, INPUT_ProjectStatus);

                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_updateProjectStatus --> OUTPUT: Updated Project {0} status to {1}", INPUT_ProjectID, INPUT_ProjectStatus);
                return true;
            }
            catch (Exception e)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_updateProjectStatus --> OUTPUT: Failed update status for project {0}", INPUT_ProjectID);
                API_DATA_Logger.LogError(e, e.Message);
                return false;
            }
        }

        [HttpGet]
        [Route("GetOutstandingProject")]
        public async Task<List<int>> API_ASYNC_EMPLOYEE_getOutstandingProjects(int INPUT_EmployeeID)
        {
            List<int> OUTPUT_outstandingProjectIDs = new List<int> { -1 };
            try
            {
                // Data Verification
                if (INPUT_EmployeeID == null || INPUT_EmployeeID < 0)
                {
                    return OUTPUT_outstandingProjectIDs;
                }
                OUTPUT_outstandingProjectIDs.Clear();

                // Logic Implementation
                OUTPUT_outstandingProjectIDs = await API_PROP_INTERFACE_LinkingTable.LINKING_ASYNC_getFromProjectEmployeeLinkingTable(INPUT_EmployeeID);
                return OUTPUT_outstandingProjectIDs;
            }
            catch (Exception e)
            {
                API_DATA_Logger.LogInformation("EXECUTED: API_ASYNC_EMPLOYEE_getOustandingProjects --> OUTPUT: Failed to get outstanding projects for employee {0}", INPUT_EmployeeID);
                API_DATA_Logger.LogError(e, e.Message);
                return OUTPUT_outstandingProjectIDs;
            }
        }
    }
}
