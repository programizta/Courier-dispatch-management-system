using Dispatch_system.Data;
using Dispatch_system.Services;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Controllers
{
    public class ClientParcelController : Controller
    {
        private readonly IClientParcelService clientParcelService;
        private readonly ApplicationDbContext context;

        public ClientParcelController(IClientParcelService clientParcelService,
            ApplicationDbContext context)
        {
            this.clientParcelService = clientParcelService;
            this.context = context;
        }

        [Authorize(Roles = "Klient")]
        [HttpGet]
        public IActionResult PostParcel()
        {
            return View();
        }

        [Authorize(Roles = "Klient")]
        [HttpPost]
        public IActionResult PostParcel(ClientParcelViewModel clientParcelViewModel)
        {
            if (ModelState.IsValid)
            {
                clientParcelService.PostOnline(clientParcelViewModel);
                int senderBranchCode = int.Parse(new string(clientParcelViewModel.SenderPostalCode.Take(2).ToArray()));

                var queryModel = (from branches in context.Branches
                                  join parcels in context.Parcels on branches.BranchId equals parcels.LastBranchId
                                  where branches.BranchCode == senderBranchCode
                                  select new ParcelSummaryViewModel
                                  {
                                      ParcelId = parcels.ParcelId,
                                      BranchName = branches.BranchName,
                                      BranchAddress = branches.BranchAddress,
                                      BranchCity = branches.City
                                  }
                    );

                ParcelSummaryViewModel model = new ParcelSummaryViewModel
                {
                    ParcelId = queryModel.First().ParcelId,
                    BranchName = queryModel.First().BranchName,
                    BranchAddress = queryModel.First().BranchAddress,
                    BranchCity = queryModel.First().BranchCity
                };

                return RedirectToAction("ThanksPage", model);
            }

            return View();
        }

        [Authorize(Roles = "Klient")]
        [HttpGet]
        public IActionResult ThanksPage(ParcelSummaryViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult CheckStatus(string parcelId)
        {
            var model = clientParcelService.CheckStatus(int.Parse(parcelId));

            if (model != null) return View(model);
            return RedirectToAction("ParcelNotFound", "Home");
        }

        [HttpGet]
        public IActionResult EnterCode()
        {
            return View();
        }
    }
}