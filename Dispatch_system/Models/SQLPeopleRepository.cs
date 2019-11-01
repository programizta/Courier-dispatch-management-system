using Dispatch_system.Data;
using Dispatch_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_system.Models
{
    public class SQLPeopleRepository : IPersonRepository
    {
        private readonly ApplicationDbContext context;

        public SQLPeopleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            var model = (from person in context.People
                         join employee in context.Employees on person.EmployeeId equals employee.EmployeeId
                         join branch in context.Branches on employee.BranchId equals branch.BranchId
                         join user in context.Users on person.UserId equals user.Id
                         join userRole in context.UserRoles on user.Id equals userRole.UserId
                         join role in context.Roles on userRole.RoleId equals role.Id
                         where person.PersonId == id
                         select new EmployeeViewModel
                         {
                             PersonId = person.PersonId,
                             EmployeeId = person.PersonId,
                             BranchName = branch.BranchName,
                             UserId = person.UserId,
                             FirstName = person.FirstName,
                             LastName = person.LastName,
                             Address = person.Address,
                             Email = user.Email,
                             City = person.City,
                             PostalCode = person.PostalCode,
                             PhoneNumber = person.PhoneNumber,
                             Role = role.Name
                         });

            EmployeeViewModel employeeModel = new EmployeeViewModel
            {
                PersonId = model.FirstOrDefault().PersonId,
                EmployeeId = model.FirstOrDefault().EmployeeId,
                BranchName = model.FirstOrDefault().BranchName,
                UserId = model.FirstOrDefault().UserId,
                FirstName = model.FirstOrDefault().FirstName,
                LastName = model.FirstOrDefault().LastName,
                Address = model.FirstOrDefault().Address,
                Email = model.FirstOrDefault().Email,
                City = model.FirstOrDefault().City,
                PostalCode = model.FirstOrDefault().PostalCode,
                PhoneNumber = model.FirstOrDefault().PhoneNumber,
                Role = model.FirstOrDefault().Role
            };

            return employeeModel;
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var model = (from person in context.People
                         join employee in context.Employees on person.EmployeeId equals employee.EmployeeId
                         join branch in context.Branches on employee.BranchId equals branch.BranchId
                         join user in context.Users on person.UserId equals user.Id
                         join userRole in context.UserRoles on user.Id equals userRole.UserId
                         join role in context.Roles on userRole.RoleId equals role.Id
                         //where role.Name != "Klient"
                         select new EmployeeViewModel
                         {
                             PersonId = person.PersonId,
                             EmployeeId = person.EmployeeId,
                             BranchName = branch.BranchName,
                             UserId = person.UserId,
                             FirstName = person.FirstName,
                             LastName = person.LastName,
                             Address = person.Address,
                             Email = user.Email,
                             City = person.City,
                             PostalCode = person.PostalCode,
                             PhoneNumber = person.PhoneNumber,
                             Role = role.Name
                         }).ToList();

            return model;
        }

        public void UpdateEmployee(Person personChanges, EmployeeViewModel employeeChanges)
        {
            // wydobycie danych osoby (pracownika) do aktualizacji
            Person personToUpdate = context.People.FirstOrDefault(x => x.PersonId == employeeChanges.EmployeeId);

            // aktualizacja danych
            if (personToUpdate != null)
            {
                personToUpdate.FirstName = personChanges.FirstName;
                personToUpdate.LastName = personChanges.LastName;
                personToUpdate.Address = personChanges.Address;
                personToUpdate.City = personChanges.City;
                personToUpdate.PostalCode = personChanges.PostalCode;
                personToUpdate.PhoneNumber = personChanges.PhoneNumber;

                context.People.Update(personToUpdate);
                context.SaveChanges();
            }

            // wydobycie nowego ID oddziału w przypadku jej zmiany
            int newBranchId = context.Branches.FirstOrDefault(x => x.BranchName == employeeChanges.BranchName).BranchId;
            // wydobycie wpisu pracownika dla aktualizacji oddziału, do którego jest przypisany
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeId == personToUpdate.EmployeeId);

            // aktualizacja oddziału pracownika
            employee.BranchId = newBranchId;
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
                var personUser = context.Users.Find(person.UserId);
                var employee = context.Employees.Find(person.EmployeeId);

                // usunięcie pracownika
                context.Remove(employee);
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
