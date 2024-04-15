namespace LedighetsPortal.Models
{
    public class EmpWithLeaveRegistration
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string LeaveType { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
