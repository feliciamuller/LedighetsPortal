using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedighetsPortal.Models
{
    public class LeaveRegistration
    {
        [Key]
        public int LeaveId { get; set; }
        [DisplayName("Ledighetstyp")]
        public string LeaveType { get; set; }
        [DisplayName("Startdatum")]
        public DateTime StartDate { get; set; }
        [DisplayName("Slutdatum")]
        public DateTime EndDate { get; set; }
        [DisplayName("Registreringsdatum")]
        public DateTime RegistrationDate { get; set; }

        [ForeignKey("Employee")]
        [DisplayName("AnställningsID")]
        public int FKEmployeeId { get; set; }
        [DisplayName("Anställd")]
        public Employee? Employee { get; set; }
    }
}
