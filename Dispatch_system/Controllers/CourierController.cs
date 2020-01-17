using Dispatch_system.Data;
using Dispatch_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dispatch_system.Controllers
{
    public class CourierController : Controller
    {
        private readonly ICourierService courierService;
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly int courierId;

        public CourierController(ICourierService courierService,
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.courierService = courierService;
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;

            courierId = GetCourierId();
        }

        /// <summary>
        /// Metoda zwracająca id kuriera
        /// </summary>
        /// <returns></returns>
        private int GetCourierId()
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return (from person in dbContext.People
                    join employee in dbContext.Employees on person.PersonId equals employee.PersonId
                    where person.UserId == userId
                    select new
                    {
                        employee.EmployeeId
                    }).First().EmployeeId;
        }

        [Authorize(Roles = "Kurier")]
        [HttpGet]
        public IActionResult ParcelsToDeliever()
        {
            var parcelsToDeliever = courierService.ParcelsToDeliever(courierId);

            return View(parcelsToDeliever);
        }

        [Authorize(Roles = "Kurier")]
        [HttpPost]
        public IActionResult Delivered(int id)
        {
            courierService.ParcelDelivered(id);
            return RedirectToAction("DeliveredSuccess");
        }

        [Authorize(Roles = "Kurier")]
        [HttpPost]
        public IActionResult FailedToDeliever(int id)
        {
            courierService.FailedToDeliever(id);
            return RedirectToAction("ParcelsToDeliever");
        }

        [Authorize(Roles = "Kurier")]
        [HttpGet]
        public IActionResult ToReturn()
        {
            var parcelsToReturn = courierService.ParcelsToReturn(courierId);

            return View(parcelsToReturn);
        }

        [Authorize(Roles = "Kurier")]
        [HttpPost]
        public IActionResult ReturnParcels()
        {
            courierService.ReturnParcelsToBranch(courierId);

            return RedirectToAction("Returned"); // zaimplementuj widok
        }

        [Authorize(Roles = "Kurier")]
        [HttpGet]
        public IActionResult ToDelieverNextDay()
        {
            var parcels = courierService.ToDelieverNextDay(courierId);

            return View(parcels);
        }
    }
}
