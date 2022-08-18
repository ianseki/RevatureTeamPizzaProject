using System.Text.RegularExpressions;

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

        public void DMODEL_CUSTOMER_verifyData()
        {
            // Check for nulls
            if (this.customer_id == null || this.customer_id < 0) throw new ArgumentNullException(nameof(this.customer_id));
            if (this.first_name == null || this.first_name == "") throw new ArgumentNullException(nameof(this.first_name));
            if (this.last_name == null || this.last_name == "") throw new ArgumentNullException(nameof(this.last_name));
            if (this.email == null || this.email == "") throw new ArgumentNullException(nameof(this.email));
            if (this.password == null || this.password == "") throw new ArgumentNullException(nameof(this.password));

            if (Regex.IsMatch(this.first_name, @"\W") || Regex.IsMatch(this.first_name, @"\d"))
            { 
                throw new ArgumentException(nameof(this.first_name)); 
            }

            if (Regex.IsMatch(this.last_name, @"\W") || Regex.IsMatch(this.last_name, @"\d"))
            {
                throw new ArgumentException(nameof(this.first_name));
            }

            if (!(Regex.IsMatch(this.email, @"@")) || !(Regex.IsMatch(this.email, @"\.")))
            {
                throw new ArgumentException(nameof(this.email) + " needs proper email format.");
            }

            if (this.password.Length < 5)
            {
                throw new ArgumentException(nameof(this.email) + " needs to at least be 5 characters");
            }
        }

    }
}