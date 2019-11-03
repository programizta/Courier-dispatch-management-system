using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public interface IEmployeeSerivce
    {
        EmployeeViewModel GetEmployee(int id);
        List<EmployeeViewModel> GetAllEmployees();
        void UpdateEmployee(Person personChanges, EmployeeViewModel employeeChanges);
        void DeleteEmployee(int id);
    }
}