using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public interface IPersonRepository
    {
        IEnumerable<EmployeeViewModel> GetEmployee(int id);
        List<EmployeeViewModel> GetAllEmployees();
        void UpdateEmployee(EmployeeViewModel personChanges);
        void DeleteEmployee(int id);
    }
}
