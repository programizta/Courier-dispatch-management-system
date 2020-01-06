using Dispatch_system.Data;
using Dispatch_system.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class CourierController : Controller
    {
        private readonly ICourierService courierService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public CourierController(ICourierService courierService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.courierService = courierService;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ParcelsToDeliever()
        {
            var user = await userManager.GetUserAsync(User);

            var courierIdModel = (from person in dbContext.People
                                  join employee in dbContext.Employees on person.PersonId equals employee.PersonId
                                  where person.UserId == user.Id
                                  select new
                                  {
                                      employee.EmployeeId
                                  }).FirstOrDefault();

            int courierId = courierIdModel.EmployeeId;
            var parcelsToDeliever = courierService.ParcelsToDeliever(courierId);

            return View(parcelsToDeliever);
        }

        [HttpPost]
        public IActionResult Delivered(int id)
        {
            courierService.ParcelDelivered(id);
            return RedirectToAction("ParcelsToDeliever");
        }
    }
}
