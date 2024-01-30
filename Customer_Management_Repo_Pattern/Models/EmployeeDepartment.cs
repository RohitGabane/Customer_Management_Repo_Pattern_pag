using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer_Management_Repo_Pattern.Models
{
    public class EmployeeDepartment
    {
        [Key]
        public int DepartmentId { get; set; } // Auto-incremented key for department ID (Primary Key)
        [MaxLength(100)]
        public string DepartmentName { get; set; } 
        public string DepartmentLocation { get; set; } 
        public string DepartmentManager { get; set; } 
                                                     
        [InverseProperty("Department")]
        public virtual ICollection<Employee> Employees { get; set; } // Department may have many employees
    }
}
