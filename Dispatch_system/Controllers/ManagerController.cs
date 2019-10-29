using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly ApplicationDbContext dbContext;

        public ManagerController(IPersonRepository personRepository, ApplicationDbContext dbContext)
        {
            this.personRepository = personRepository;
            this.dbContext = dbContext;
        }

        public IActionResult AllEmployees()
        {
            var employees = personRepository.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var model = personRepository.GetEmployee(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = personRepository.GetEmployee(model.EmployeeId);

                employee.First().FirstName = model.FirstName;
                employee.First().LastName = model.FirstName;
                employee.First().Address = model.FirstName;
                employee.First().City = model.FirstName;
                employee.First().PostalCode = model.FirstName;
                employee.First().PhoneNumber = model.FirstName;
                employee.First().Email = model.FirstName;
            }
        }
    }
}
