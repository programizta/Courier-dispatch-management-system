using Dispatch_system.ViewModels;

namespace Dispatch_system.Services
{
    public interface IAccountService
    {
        int? GetPersonId(string email);

        PersonDataViewModel GetPersonalData(int personId);
    }
}
