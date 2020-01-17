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
    public class WarehousemanController : Controller
    {
        private readonly IWarehouseService warehouseService;
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly short branchId;

        public WarehousemanController(IWarehouseService warehouseService,
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.warehouseService = warehouseService;
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;

            branchId = GetBranchId();
        }

        /// <summary>
        /// Metoda pobierająca id oddziału pracownika, który się zaloguje na swoje konto
        /// </summary>
        /// <returns></returns>
        private short GetBranchId()
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return (from person in dbContext.People
                    join employee in dbContext.Employees on person.PersonId equals employee.PersonId
                    where person.UserId == userId
                    select new
                    {
                        employee.BranchId
                    }).First().BranchId;
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpPost]
        public IActionResult AcceptSentParcels()
        {
            warehouseService.AcceptSentParcels(branchId);
            return RedirectToAction("ParcelsAccepted");
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpGet]
        public IActionResult ParcelsAccepted()
        {
            return View();
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpGet]
        public IActionResult ParcelsToRegister()
        {
            var newParcels = warehouseService.NewParcelsToRegister(branchId);

            return View(newParcels);
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpGet]
        public IActionResult Couriers()
        {
            var listOfCouriers = warehouseService.CouriersInBranch(branchId);

            return View(listOfCouriers);
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpGet]
        public IActionResult Parcels(int id)
        {
            var listOfParcels = warehouseService.CourierParcels(id);

            return View(listOfParcels);
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpPost]
        public IActionResult TransferParcelsToCourier(int id)
        {
            warehouseService.GiveCourierParcels(id);
            return RedirectToAction("Transferred"); // zaimplementuj widok
        }

        [Authorize(Roles = "Pracownik sortowni")]
        [HttpGet]
        public IActionResult Transferred()
        {
            return View();
        }
    }
}
