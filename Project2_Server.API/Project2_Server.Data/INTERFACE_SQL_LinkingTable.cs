using Project2_Server.Model;

namespace Project2_Server.Data
{
    public interface INTERFACE_SQL_LinkingTable
    {
        Task<bool> LINKING_ASYNC_addToCustomerOrderLinkingTable(int INPUT_CustomerID, int INPUT_OrderID);
        // FUNCTION:
        //      Adds an entry to the Customer-Order Linking Table
        // PARAMETER: (int, int)
        //      Customer ID
        //      Order ID
        // OUTPUT: (bool)
        //      If successfully add to linking table --> OUTPUT: returns true
        //          OR
        //      If not able to add to linking table --> OUTPUT: returns false


        Task<List<int>> LINKING_ASYNC_getFromCustomerOrderLinkingTable(int INPUT_CustomerID);
        // FUNCTION:
        //      Gets all linked orders corresponding to an customer
        // PARAMETER: (int)
        //      Customer ID
        // OUTPUT: (bool)
        //      If finds orders corresponding to the customer --> OUTPUT: returns array with order ids
        //          OR
        //      If not able find corresponding orders --> OUTPUT: returns dummy array (-1)

        Task<bool> LINKING_ASYNC_addToOrderProjectLinkingTable(int INPUT_OrderID, int INPUT_ProjectID);
        // FUNCTION:
        //      Adds an entry to the Order-Project Linking Table
        // PARAMETER: (int, int)
        //      Order ID
        //      Project ID
        // OUTPUT: (bool)
        //      If successfully add to linking table --> OUTPUT: returns true
        //          OR
        //      If not able to add to linking table --> OUTPUT: returns false

        Task<List<int>> LINKING_ASYNC_getFromOrderProjectLinkingTable(int INPUT_OrderID);
        // FUNCTION:
        //      Gets all linked projects corresponding to an order
        // PARAMETER: (int)
        //      Order ID
        // OUTPUT: (bool)
        //      If finds project corresponding to the order --> OUTPUT: returns array with project ids
        //          OR
        //      If not able find corresponding project --> OUTPUT: returns dummy array (-1)

        Task<bool> LINKING_ASYNC_addToProjectEmployeeLinkingTable_Chair(int INPUT_ProjectID);
        Task<bool> LINKING_ASYNC_addToProjectEmployeeLinkingTable_Table(int INPUT_ProjectID);
        Task<bool> LINKING_ASYNC_addToProjectEmployeeLinkingTable_Desk(int INPUT_ProjectID);
        // FUNCTION:
        //      Adds 1 employee with the least amount of jobs to project.
        //      Loops through as many times as the employees needed for item.
        // PARAMETER: (int)
        //      Project ID
        // OUTPUT: (bool)
        //      If INPUT_ProjectID is good then run SQL command and return true
        //          OR
        //      If not return false

        Task<List<int>> LINKING_ASYNC_getFromProjectEmployeeLinkingTable(int INPUT_EmployeeID);
        // FUNCTION:
        //      Gets all linked outstanding projects corresponding to an employee
        // PARAMETER: (int)
        //      Employee ID
        // OUTPUT: (bool)
        //      If finds projects corresponding to the employee --> OUTPUT: returns array with order ids
        //          OR
        //      If not able find corresponding projects --> OUTPUT: returns dummy array (-1)
    }
}
