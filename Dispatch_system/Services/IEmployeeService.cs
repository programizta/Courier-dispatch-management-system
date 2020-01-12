using Dispatch_system.ViewModels;
using System.Collections.Generic;

namespace Dispatch_system.Models
{
    public interface IEmployeeSerivce
    {
        EmployeeViewModel GetEmployee(int id);
        List<EmployeeViewModel> GetAllEmployees();
        void UpdateEmployee(EmployeeViewModel employeeChanges);
        void DeleteEmployee(int id);
    }
}