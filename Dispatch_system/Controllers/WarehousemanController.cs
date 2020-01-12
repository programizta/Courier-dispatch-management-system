using Dispatch_system.Data;
using Dispatch_system.Services;
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

        /// <summary>
        /// Wyświetlenie listy kurierów z określonego oddziału.
        /// Pracownik węzła może zobaczyć tylko kurierów, którzy mają zgodny
        /// identyfikator placówki
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Couriers()
        {
            var couriers = warehouseService.CouriersInBranch(branchId);

            return View(couriers); // zaimplementuj widok
        }

        /// <summary>
        /// Akceptacja przysłanych przesyłek do węzła ekspedycyjno-rozdzielczego
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AcceptSentParcels()
        {
            warehouseService.AcceptSentParcels(branchId);
            return RedirectToAction("ParcelsAccepted"); // zaimplementuj widok
        }
    }
}
