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
        private readonly IEmployeeSerivce employeeRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public ManagerController(IEmployeeSerivce employeeRepository, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.employeeRepository = employeeRepository;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult AllEmployees()
        {
            var employees = employeeRepository.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var model = employeeRepository.GetEmployee(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(model.FirstName);
            string firstName = System.Text.Encoding.ASCII.GetString(bytes);

            bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(model.LastName);
            string lastName = System.Text.Encoding.ASCII.GetString(bytes);

            model.Email = firstName.ToLower() + "."
                + lastName.ToLower() + "@poczta.pl";
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };

            var result = await userManager.CreateAsync(user, "9384HF#77!o"); // zahardcode'owane nowe hasło
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, model.Role);

                string userId = userManager
                                    .Users.FirstOrDefault(u => u.Email == model.Email)
                                    .Id;

                short branchId = context.Branches.FirstOrDefault(x => x.BranchName == model.BranchName).BranchId;

                Employee employee = new Employee
                {
                    BranchId = branchId
                };

                context.Add(employee);
                context.SaveChanges();

                Person newPerson = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    PhoneNumber = model.PhoneNumber,
                    UserId = userId,
                    EmployeeId = employee.EmployeeId
                };

                context.Add(newPerson);
                context.SaveChanges();

            }

            return View();
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeRepository.GetEmployee(model.EmployeeId);

                Person person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    PhoneNumber = model.PhoneNumber
                };

                employeeRepository.UpdateEmployee(person, model);
            }

            return RedirectToAction("AllEmployees", "Manager");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            employeeRepository.DeleteEmployee(id);

            return RedirectToAction("AllEmployees", "Manager");
        }
    }
}