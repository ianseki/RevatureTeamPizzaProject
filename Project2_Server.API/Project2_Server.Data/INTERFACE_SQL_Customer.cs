using Project2_Server.Model;

namespace Project2_Server.Data
{
    public interface INTERFACE_SQL_Customer
    {
        Task<DMODEL_Customer> CUSTOMER_ASYNC_getCustomerData(int INPUT_CustomerID);
        // FUNCTION:
        //      Gets the all the customer data for an inputed customer ID and returns it as a DMODEL_Customer
        // PARAMETER (int):
        //      Customer's Id
        // OUTPUT (DMODEL_Customer):
        //      If corresponding user is found -> returns customer's data row
        //          OR
        //      If no corresponding user is found -> returns blank user (-1, "", "", "", "")


       // public DMODEL_Customer GetCustomer(int INPUT_CustomerID);

        //public void CreateDMODEL_Customer (DMODEL_Customer customer);
       /// public void UpdateDMODEL_Customer (int INPUT_CustomerID, DMODEL_Customer customer);   
        //public void DeleteDMODEL_Customer (int INPUT_CustomerID);

        Task<DMODEL_Customer> CUSTOMER_ASYNC_checkCustomerLogin(string INPUT_Email);
        // FUNCTION:
        //      Passes the inputed email coresponds to an entry/row in the [Project2].[Customer] database
        // PARAMETER (string, string):
        //      Customer's Email
        // OUTPUT (DMODEL_Customer):
        //      If valid login -> returns the customer's data row
        //          OR
        //      If no corresponding data is found -> returns a blank user (-1, "", "", "", "")

        Task<bool> CUSTOMER_ASYNC_createNewCustomer(DMODEL_Customer INPUT_DMODEL_Customer);
        // FUNCTION:
        //      Inserts into the [Project2].[Customer] database a new user
        // PARAMETER (DMODEL_Customer):
        //      Customer's data in a DMODEL_Customer
        //          *NOTE: The entered customerID in the passed in Data Model is disregared / dummy data
        //                      as the database with auto-generate its own customerID
        // FUNCTION (bool):
        //      If succesfully able to create new customer -> returns TRUE
        //          OR
        //      If not able to create new employee -> returns FALSE


    }
}