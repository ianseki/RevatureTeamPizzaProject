using Project2_Server.Model;

namespace Project2_Server.Data
{
    public interface INTERFACE_SQL_Employee
    {
        Task<DMODEL_Employee> EMPLOYEE_ASYNC_getEmployeeData(int INPUT_EmployeeID);
        // FUNCTION:
        //      Gets the all the employee data for an inputed customer ID and returns it as a DMODEL_Employee
        // PARAMETER (int):
        //      Customer's Id
        // OUTPUT (DMODEL_Employee):
        //      If corresponding user is found -> returns employee's data row
        //          OR
        //      If no corresponding user is found -> returns blank user (-1, "", "", "", "")


        Task<bool> EMPLOYEE_ASYNC_createNewEmployee(DMODEL_Employee INPUT_DMODEL_Employee);
        // FUNCTION:
        //      Inserts into the [Project2].[Employee] database a new user
        // PARAMETER (DMODEL_Employee):
        //      Employee's data in a DMODEL_Employee
        //          *NOTE: The entered employeeID in the passed in Data Model is disregared / dummy data
        //                      as the database with auto-generate its own employeeID
        // FUNCTION (bool):
        //      If succesfully able to create new employee -> returns TRUE
        //          OR
        //      If not able to create new employee -> returns FALSE

        
        Task<DMODEL_Employee> EMPLOYEE_ASYNC_checkEmployeeLogin(string INPUT_Email, string INPUT_Password);
        // FUNCTION:
        //      Verifies that the inputed email and password coresponds to an entry/row in the [Project2].[Employee] database
        // PARAMETER (string, string):
        //      Employee's Email
        //      Employee's Password
        // OUTPUT (DMODEL_Employee):
        //      If valid login -> returns the employee's data row
        //          OR
        //      If no corresponding data is found -> returns a blank user (-1, "", "", "", "")
    }
}
