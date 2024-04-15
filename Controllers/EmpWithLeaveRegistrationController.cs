using LedighetsPortal.Data;
using LedighetsPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LedighetsPortal.Controllers
{
    public class EmpWithLeaveRegistrationController : Controller
    {
        private readonly LeaveDbContext _context;
        public EmpWithLeaveRegistrationController(LeaveDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> LeaveHistory(int? leaveId)
        {
            var leaveList = await _context.LeaveRegistrations.ToListAsync();

            var query = from leave in _context.LeaveRegistrations
                        join emp in _context.Employees on leave.FKEmployeeId
                        equals emp.EmployeeId
                        select new { emp, leave };

            if (leaveId.HasValue)
            {
                query = query.Where(x => x.leave.LeaveId == leaveId.Value);
            }

            var employees = await query.Select(x => new EmpWithLeaveRegistration
            {
                EmployeeFirstName = x.emp.EmployeeFirstName,
                EmployeeLastName = x.emp.EmployeeLastName,
                LeaveType = x.leave.LeaveType,
                RegistrationDate = x.leave.RegistrationDate

            }).ToListAsync();

            //adding the filtered data in viewModel
            var viewModel = new EmployeeLeaveViewModel
            {
                Employees = employees,
                LeaveRegistrations = leaveList,
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> FindEmployeeLeave(int? employeeId)
        {
            //list of all employees to print out in dropdownlist
            var allEmployees = await _context.Employees.ToListAsync();
            //sending all employees to view
            ViewBag.AllEmployees = allEmployees;

            var leaveListForEmp = _context.LeaveRegistrations.ToList();

            var query = from leave in _context.LeaveRegistrations
                        join emp in _context.Employees on leave.FKEmployeeId
                        equals emp.EmployeeId
                        select new { emp, leave };

            if (employeeId.HasValue)
            {
                query = query.Where(x => x.emp.EmployeeId == employeeId.Value);
            }

            var employees = await query.Select(x => new EmpWithLeaveRegistration
            {
                EmployeeFirstName = x.emp.EmployeeFirstName,
                LeaveType = x.leave.LeaveType,
                EmployeeId = x.emp.EmployeeId,

            }).ToListAsync();

            //adding the filtered data to viewModel
            var viewModel = new EmployeeLeaveViewModel
            {
                Employees = employees,
                LeaveRegistrations = leaveListForEmp,
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> FindLeaveRegMonth(int selectedMonth)
        {
            //list of filtered data depending on selectedMonth
            var leaveListMonth = await _context.LeaveRegistrations
                .Where(x => x.RegistrationDate.Month == selectedMonth)
                .ToListAsync();

            var query = from leave in leaveListMonth
                        join emp in _context.Employees on leave.FKEmployeeId 
                        equals emp.EmployeeId
                        select new { emp, leave };

            var employees = query.Select(x => new EmpWithLeaveRegistration
            {
                EmployeeFirstName = x.emp.EmployeeFirstName,
                LeaveType = x.leave.LeaveType,
                EmployeeId = x.emp.EmployeeId,
            }).ToList();

            //adding the filtered data to viewModel
            var viewModel = new EmployeeLeaveViewModel
            {
                Employees = employees,
                LeaveRegistrations = leaveListMonth,
            };
            return View(viewModel);
        }
    }
}
