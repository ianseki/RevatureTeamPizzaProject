namespace Project2_Server.Model
{
    // FIELDS
    public class DMODEL_Employee
    {
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        // CONSTRUCTORS
        public DMODEL_Employee() { }
        public DMODEL_Employee(int INPUT_EmployeeID, string INPUT_Firstname, string INPUT_Lastname, string INPUT_Email, string INPUT_Passwrord)
        {
            this.employee_id = INPUT_EmployeeID;
            this.first_name = INPUT_Firstname;
            this.last_name = INPUT_Lastname;
            this.email = INPUT_Email;
            this.password = INPUT_Passwrord;
        }

        // METHODS
        public void DMODEL_DEBUG_printCustomer()
        {
            Console.WriteLine(employee_id);
            Console.WriteLine(first_name);
            Console.WriteLine(last_name);
            Console.WriteLine(email);
            Console.WriteLine(password);
        }

    }
}