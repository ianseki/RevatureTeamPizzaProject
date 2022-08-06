using Project2_Server.Model;

namespace Project2_Server.Data
{
    public interface INTERFACE_SQL_Project
    {
        Task<DMODEL_Project> PROJECT_ASYNC_getProjectData(int INPUT_ProjectID);
        // FUNCTION:
        //      Gets the project data for the corresponding inputed project ID and returns a DMODEL_Project
        // PARAMETERS: (int)
        //      Project ID
        // OUTPUTS: (DMODEL_Project)
        //      If corresponding project is found -> returns project data
        //          OR
        //      If no corresponding project is found -> returns blank project (-1, -1, false)

        Task<bool> PROJECT_ASYNC_createNewProject(DMODEL_Project INPUT_DMODEL_Project);
        // FUNCTION:
        //      Get project data and tries to enter it into Project Database
        // PARAMETERS: (DMODEL_Order)
        //      Project DateTime
        //      Project Status
        //           *NOTE: The entered projectID in the passed in Data Model is disregared / dummy data
        //                      as the database with auto-generate its own projectID
        // OUTPUTS: (bool)
        //      If succesfully created a new project --> OUTPUT: returns true
        //          OR
        //      If unable to create a new project --> OUTPUT: returns false

        Task<bool> PROJECT_ASYNC_changeProjectStatus(int INPUT_ProjectID, bool INPUT_Status);
        // FUNCTION:
        //      Changes the status of the inputed project id
        // PARAMETERS: (int, bool)
        //      Project ID
        //      Project Status
        // OUTPUTS: (bool)
        //      If succesfully changed the status for project --> OUTPUT: returns true
        //          OR
        //      If unable to changed the status for project --> OUTPUT: returns false
    }
}
