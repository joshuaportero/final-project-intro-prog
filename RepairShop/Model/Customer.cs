namespace RepairShop.Model
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }

        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string email, string cellphone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Cellphone = cellphone;
        }

        public override string ToString()
        {
            return "Customer { \n  FirstName: " + FirstName + "\n  LastName: " + LastName + "\n  Email: " + Email +
                   "\n  Cellphone: " + Cellphone + "\n}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Customer)) return false;
            var target = (Customer)obj;
            return target.Email.Equals(Email) || target.Cellphone.Equals(Cellphone);
        }

        /**
         * Email and Cellphone should be read-only because the
         * hashcode should not change during runtime.
         *
         * It seems to work fine under the program circumstances.
         */
        public override int GetHashCode()
        {
            return Email.GetHashCode() * 17 + Cellphone.GetHashCode();
        }
    }

    public class CustomerBuilder
    {
        private readonly Customer _customer;

        public CustomerBuilder()
        {
            _customer = new Customer();
        }

        public CustomerBuilder WithFirstName(string firstName)
        {
            _customer.FirstName = firstName;
            return this;
        }

        public CustomerBuilder WithLastName(string lastName)
        {
            _customer.LastName = lastName;
            return this;
        }


        public CustomerBuilder WithEmail(string email)
        {
            _customer.Email = email;
            return this;
        }


        public CustomerBuilder WithCellphone(string cellphone)
        {
            _customer.Cellphone = cellphone;
            return this;
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}