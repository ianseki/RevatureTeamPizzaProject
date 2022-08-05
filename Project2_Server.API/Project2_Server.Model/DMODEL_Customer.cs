namespace Project2_Server.Model
{
    // FIELDS
    public class DMODEL_Customer
    {
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        // CONSTRUCTORS
        public DMODEL_Customer() { }
        public DMODEL_Customer(int INPUT_CustomerID, string INPUT_Firstname, string INPUT_Lastname, string INPUT_Email, string INPUT_Passwrord)
        {
            this.customer_id = INPUT_CustomerID;
            this.first_name = INPUT_Firstname;
            this.last_name = INPUT_Lastname;
            this.email = INPUT_Email;
            this.password = INPUT_Passwrord;
        }

        // METHODS
        public void DMODEL_DEBUG_printCustomer()
        {
            Console.WriteLine(customer_id);
            Console.WriteLine(first_name);
            Console.WriteLine(last_name);
            Console.WriteLine(email);
            Console.WriteLine(password);
        }

    }
}