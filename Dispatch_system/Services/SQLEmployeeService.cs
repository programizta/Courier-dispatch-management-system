using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Dispatch_system.Models
{
    public class SQLEmployeeService : IEmployeeSerivce
    {
        private readonly ApplicationDbContext context;

        public SQLEmployeeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            var model = (from person in context.People
                         join address in context.UserAddresses on person.PersonId equals address.PersonId
                         join employee in context.Employees on person.PersonId equals employee.PersonId
                         join branch in context.Branches on employee.BranchId equals branch.BranchId
                         join user in context.Users on person.UserId equals user.Id
                         join userRole in context.UserRoles on user.Id equals userRole.UserId
                         join role in context.Roles on userRole.RoleId equals role.Id
                         where employee.EmployeeId == id
                         select new EmployeeViewModel
                         {
                             PersonId = person.PersonId,
                             EmployeeId = employee.EmployeeId,
                             BranchName = branch.BranchName,
                             UserId = person.UserId,
                             FirstName = person.FirstName,
                             LastName = person.LastName,
                             StreetName = address.StreetName,
                             BlockNumber = address.BlockNumber,
                             FlatNumber = address.FlatNumber,
                             PostalCode = address.PostalCode,
                             City = address.City,
                             Email = user.Email,
                             PhoneNumber = person.PhoneNumber,
                             Role = role.Name
                         });

            return model.First();
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var model = (from person in context.People
                         join address in context.UserAddresses on person.PersonId equals address.PersonId
                         join employee in context.Employees on person.PersonId equals employee.PersonId
                         join branch in context.Branches on employee.BranchId equals branch.BranchId
                         join user in context.Users on person.UserId equals user.Id
                         join userRole in context.UserRoles on user.Id equals userRole.UserId
                         join role in context.Roles on userRole.RoleId equals role.Id
                         select new EmployeeViewModel
                         {
                             PersonId = person.PersonId,
                             EmployeeId = employee.EmployeeId,
                             BranchName = branch.BranchName,
                             UserId = person.UserId,
                             FirstName = person.FirstName,
                             LastName = person.LastName,
                             StreetName = address.StreetName,
                             BlockNumber = address.BlockNumber,
                             FlatNumber = address.FlatNumber,
                             PostalCode = address.PostalCode,
                             City = address.City,
                             Email = user.Email,
                             PhoneNumber = person.PhoneNumber,
                             Role = role.Name
                         }).ToList();

            return model;
        }

        public void UpdateEmployee(EmployeeViewModel employeeChanges)
        {
            // wydobycie danych osoby (pracownika) do aktualizacji
            Person personToUpdate = context.People.FirstOrDefault(x => x.PersonId == employeeChanges.PersonId);

            UserAddress personsAddress = context.UserAddresses.FirstOrDefault(x => x.PersonId == personToUpdate.PersonId);

            // aktualizacja danych
            if (personToUpdate != null)
            {
                personToUpdate.FirstName = employeeChanges.FirstName;
                personToUpdate.LastName = employeeChanges.LastName;
                personToUpdate.PhoneNumber = employeeChanges.PhoneNumber;
                context.People.Update(personToUpdate);
                context.SaveChanges();

                personsAddress.StreetName = employeeChanges.StreetName;
                personsAddress.BlockNumber = employeeChanges.BlockNumber;
                personsAddress.FlatNumber = employeeChanges.FlatNumber;
                personsAddress.PostalCode = employeeChanges.PostalCode;
                personsAddress.City = employeeChanges.City;
                context.SaveChanges();
            }

            // wydobycie nowego ID oddziału w przypadku jej zmiany
            short newBranchId = context.Branches.FirstOrDefault(x => x.BranchName == employeeChanges.BranchName).BranchId;
            // wydobycie wpisu pracownika dla aktualizacji oddziału, do którego jest przypisany
            var employee = context.Employees.FirstOrDefault(x => x.PersonId == personToUpdate.PersonId);

            // aktualizacja oddziału pracownika
            employee.BranchId = newBranchId;

            // jeśli pracownik będzie od teraz kurierem, oznaczenie go jako kurier
            if (employeeChanges.Role == "Kurier") employee.IsCourier = true;
            else employee.IsCourier = false;

            context.SaveChanges();

            // wydobycie roli dla użytkownika, jej usunięcie
            var userRole = context.UserRoles.FirstOrDefault(x => x.UserId == employeeChanges.UserId);
            context.UserRoles.Remove(userRole);
            context.SaveChanges();

            // aktualzacja roli pracownika
            userRole.UserId = employeeChanges.UserId;
            userRole.RoleId = context.Roles.FirstOrDefault(x => x.Name == employeeChanges.Role).Id;
            context.Add(userRole);
            context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employeeModel = GetEmployee(id);

            if (employeeModel != null)
            {
                var person = context.People.Find(id);
                var address = context.UserAddresses.FirstOrDefault(x => x.PersonId == id); // do ogarnięcia
                var personUser = context.Users.Find(person.UserId);
                var employee = context.Employees.FirstOrDefault(x => x.PersonId == id);

                // usunięcie pracownika
                context.Remove(employee);
                context.SaveChanges();

                // usunięcie adresu osoby
                context.Remove(address);
                context.SaveChanges();

                // usunięcie osoby
                context.Remove(person);
                context.SaveChanges();

                // usunięcie konta
                context.Remove(personUser);
                context.SaveChanges();
            }
        }
    }
}
