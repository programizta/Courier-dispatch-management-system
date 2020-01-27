using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IEmployeeSerivce employeeRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public ManagerController(IEmployeeSerivce employeeRepository,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.employeeRepository = employeeRepository;
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Kierownik")]
        [HttpGet]
        public IActionResult AllEmployees()
        {
            var employees = employeeRepository.GetAllEmployees();
            return View(employees);
        }

        [Authorize(Roles = "Kierownik")]
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var model = employeeRepository.GetEmployee(id);
            return View(model);
        }

        [Authorize(Roles = "Kierownik")]
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [Authorize(Roles = "Kierownik")]
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

                Person newPerson = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserId = userId
                };

                context.Add(newPerson);
                context.SaveChanges();

                int personId = context.People.FirstOrDefault(x => x.PersonId == newPerson.PersonId).PersonId;

                UserAddress userAddress = new UserAddress
                {
                    PersonId = personId,
                    StreetName = model.StreetName,
                    BlockNumber = model.BlockNumber,
                    FlatNumber = model.FlatNumber,
                    PostalCode = model.PostalCode,
                    City = model.City
                };

                context.Add(userAddress);
                context.SaveChanges();

                Employee newEmployee = new Employee
                {
                    PersonId = personId,
                    BranchId = branchId,
                    IsCourier = model.Role == "Kurier" ? true : false
                };

                context.Add(newEmployee);
                context.SaveChanges();
            }

            return View();
        }

        [Authorize(Roles = "Kierownik")]
        [HttpPost]
        public IActionResult EditEmployee(EmployeeViewModel employeeChanges)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeRepository.GetEmployee(employeeChanges.EmployeeId);
                employeeRepository.UpdateEmployee(employeeChanges);
            }

            return RedirectToAction("AllEmployees", "Manager");
        }

        [Authorize(Roles = "Kierownik")]
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            employeeRepository.DeleteEmployee(id);
            return RedirectToAction("AllEmployees", "Manager");
        }
    }
}