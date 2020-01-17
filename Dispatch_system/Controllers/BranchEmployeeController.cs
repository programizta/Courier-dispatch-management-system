using Dispatch_system.Data;
using Dispatch_system.Services;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dispatch_system.Controllers
{
    public class BranchEmployeeController : Controller
    {
        private readonly IBranchParcelService parcelService;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly short branchId;

        public BranchEmployeeController(IBranchParcelService parcelService,
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.parcelService = parcelService;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;

            branchId = GetBranchId();
        }

        private short GetBranchId()
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return (from person in context.People
                    join employee in context.Employees on person.PersonId equals employee.PersonId
                    where person.UserId == userId
                    select new
                    {
                        employee.BranchId
                    }).First().BranchId;
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult OnlineOrders()
        {
            var notSentParcels = parcelService.NotSentParcels(branchId);

            return View(notSentParcels);
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult CompleteData(int id)
        {
            var parcel = parcelService.GetParcel(id);
            return View(parcel);
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpPost]
        public IActionResult CompleteData(ClientParcelViewModel parcelModel)
        {
            if (ModelState.IsValid)
            {
                parcelService.Post(parcelModel);
            }
            return RedirectToAction("Sent");
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult RegisterParcel()
        {
            return View();
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpPost]
        public IActionResult RegisterParcel(ClientParcelViewModel parcelViewModel)
        {
            if (ModelState.IsValid)
            {
                parcelService.RegisterParcel(parcelViewModel);
                return RedirectToAction("Sent");
            }

            return View();
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult Sent()
        {
            return View();
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult ParcelDelivered()
        {
            return View();
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpPost]
        public IActionResult ParcelDelivered(int parcelId)
        {
            parcelService.MarkAsDelivered(parcelId);
            return RedirectToAction("ParcelDelivered");
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult ParcelsToSend()
        {
            var parcelsToSend = parcelService.ParcelsToSend(branchId);

            return View(parcelsToSend);
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpPost]
        public IActionResult SendParcelsToMainBranch()
        {
            parcelService.SendParcelsToMainBranch(branchId);

            return RedirectToAction("ParcelsSent");
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult ParcelsSent()
        {
            return View();
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpGet]
        public IActionResult ParcelsToPick()
        {
            var parcels = parcelService.ParcelsToPick(branchId);

            return View(parcels);
        }

        [Authorize(Roles = "Pracownik oddziału")]
        [HttpPost]
        public IActionResult MarkAsDelivered(int id)
        {
            parcelService.MarkAsDelivered(id);
            return RedirectToAction("ParcelDelivered");
        }
    }
}
