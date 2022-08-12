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

        public void DMODEL_EMPLOYEE_verifyData()
        {
            if (this.employee_id == null || this.employee_id < 0) throw new ArgumentNullException(nameof(this.employee_id));
            if (this.first_name == null || this.first_name == "") throw new ArgumentNullException(nameof(this.first_name));
            if (this.last_name == null || this.last_name == "") throw new ArgumentNullException(nameof(this.last_name));
            if (this.email == null || this.email == "") throw new ArgumentNullException(nameof(this.email));
            if (this.password == null || this.password == "") throw new ArgumentNullException(nameof(this.password));
        }

    }
}