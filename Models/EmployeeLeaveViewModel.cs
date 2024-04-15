namespace LedighetsPortal.Models
{
    public class EmployeeLeaveViewModel
    {
        public IEnumerable<EmpWithLeaveRegistration> Employees { get; set; }
        public IEnumerable<LeaveRegistration> LeaveRegistrations { get; set; }
    }
}
