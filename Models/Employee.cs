using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedighetsPortal.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [DisplayName("Förnamn")]
        public string EmployeeFirstName { get; set; }
        [DisplayName("Efternamn")]
        public string EmployeeLastName { get; set; }
        public virtual ICollection<LeaveRegistration>? LeaveRegistration { get; set; }
    }
}
