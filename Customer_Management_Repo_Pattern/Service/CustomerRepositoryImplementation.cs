using Customer_Management_Repo_Pattern.Models;
using System.Linq;
namespace Customer_Management_Repo_Pattern.Service
{
    public class CustomerRepositoryImplementation : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepositoryImplementation(CustomerDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }
        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }
        public bool IsEmailOrPhoneNumberExists(string email, string phoneNumber, int customerId)
        {
            // Check if email or phone number already exists for customers excluding the current customer
            return _context.Customers.Any(c => (c.Email == email || c.PhoneNumber == phoneNumber) && c.CustomerId != customerId);
        }
        public bool IsEmailOrPhoneNumberExists(string email, string phoneNumber)
        {
            // Check if email or phone number already exists for any customer
            return _context.Customers.Any(c => c.Email == email || c.PhoneNumber == phoneNumber);
        }
        public void Add(Customer customer)
        {
            // Check if email or phone number already exists
            if (_context.Customers.Any(c => c.Email == customer.Email || c.PhoneNumber == customer.PhoneNumber))
            {
                Console.WriteLine("Customer with the same email or phone number already exists.");
                return;
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void Update(Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
