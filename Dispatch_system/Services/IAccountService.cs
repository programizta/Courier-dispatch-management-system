using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Services
{
    public interface IAccountService
    {
        int? GetPersonId(string email);

        PersonDataViewModel GetPersonalData(int personId);
    }
}
