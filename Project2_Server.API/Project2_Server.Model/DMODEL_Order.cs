﻿namespace Project2_Server.Model
{
    public class DMODEL_Order
    {
        // FIELDS
        public int order_id { get; set; }
        public DateTime time_of_order { get; set; }
        public bool status { get; set; }

        // CONSTRUCTORS
        public DMODEL_Order() { }
        public DMODEL_Order(int INPUT_OrderID, DateTime INPUT_DateTime, bool INPUT_status)
        {
            this.order_id = INPUT_OrderID;
            this.time_of_order = INPUT_DateTime;
            this.status = INPUT_status;
        }

        // METHODS
        public void DMODEL_DEBUG_printOrder()
        {
            Console.WriteLine(this.order_id);
            Console.WriteLine(this.time_of_order);
            Console.WriteLine(this.status);
        }

    }
}
