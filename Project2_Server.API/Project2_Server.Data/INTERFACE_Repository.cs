// DOCUMENTATION
//
//
//
//
//
//
//
//

using Project2_Server.Model;

namespace Project2_Server.Data
{
    public class INTERFACE_Repository
    {
        Task<DMODEL_Customer> CUSTOMER_ASYNC_getCustomerData(int INPUT_CustomerID);
        // PARAMETER:
        //      Customer Id
        // OUTPUT:
        //      Blank DMODEL_Customer (-1, "", "", "", "")
        //          OR
        //      Customer's Information
    }
}