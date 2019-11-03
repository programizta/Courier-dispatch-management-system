using Dispatch_system.Data;
using Dispatch_system.Services;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Controllers
{
    public class ClientParcelController : Controller
    {
        private readonly IClientParcelService clientParcelService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public ClientParcelController(IClientParcelService clientParcelService, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.clientParcelService = clientParcelService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult PostParcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostParcel(ClientParcelViewModel clientParcelViewModel)
        {
            clientParcelService.ParcelPost(clientParcelViewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}