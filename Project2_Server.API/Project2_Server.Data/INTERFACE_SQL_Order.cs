using Project2_Server.Model;

namespace Project2_Server.Data
{
    public interface INTERFACE_SQL_Order
    {
        Task<DMODEL_Order> ORDER_ASYNC_getOrderData(int INPUT_OrderID);
        // FUNCTION:
        //      Gets the order data for the corresponding inputed order ID and returns a DMODEL_Order
        // PARAMETERS: (int)
        //      Order ID
        // OUTPUTS: (DMODEL_Order)
        //      If corresponding order is found -> returns order data
        //          OR
        //      If no corresponding order is found -> returns blank order (-1, DateTime.MinValue, false)

        Task<int> ORDER_ASYNC_createNewOrder(DMODEL_Order INPUT_DMODEL_Order);
        // FUNCTION:
        //      Get order data and tries to enter it into Order Database
        // PARAMETERS: (DMODEL_Order)
        //      Order DateTime
        //      Order Status
        //           *NOTE: The entered orderID in the passed in Data Model is disregared / dummy data
        //                      as the database with auto-generate its own orderID
        // OUTPUTS: (int)
        //      If succesfully created a new order --> OUTPUT: returns generated order's primary key
        //          OR
        //      If unable to create a new order --> OUTPUT: returns -1

        Task<bool> ORDER_ASYNC_changeOrderStatus(int INPUT_OrderID, bool INPUT_Status);
        // FUNCTION:
        //      Changes the status of the inputed order id
        // PARAMETERS: (int, bool)
        //      Order ID
        //      Order Status
        // OUTPUTS: (bool)
        //      If succesfully changed the status for order --> OUTPUT: returns true
        //          OR
        //      If unable to changed the status for order --> OUTPUT: returns false
    }
}
