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

        public IEnumerable<EmployeeViewModel> GetEmployee(int id)
        {
            var model = (from a in context.People
                         join b in context.Users on a.UserId equals b.Id
                         join c in context.UserRoles on b.Id equals c.UserId
                         join d in context.Roles on c.RoleId equals d.Id
                         where a.PersonId == id
                         select new EmployeeViewModel
                         {
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Address = a.Address,
                             Email = b.Email,
                             City = a.City,
                             PostalCode = a.PostalCode,
                             PhoneNumber = a.PhoneNumber,
                             Role = d.Name
                         }).AsEnumerable();

            return model;
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var model = (from a in context.People
                         join b in context.Users on a.UserId equals b.Id
                         join c in context.UserRoles on b.Id equals c.UserId
                         join d in context.Roles on c.RoleId equals d.Id
                         //where d.Name != "Klient"
                         select new EmployeeViewModel
                         {
                             EmployeeId = a.PersonId,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Address = a.Address,
                             City = a.City,
                             PostalCode = a.PostalCode,
                             PhoneNumber = a.PhoneNumber,
                             Role = d.Name
                         }).ToList();

            return model;
        }

        public void UpdateEmployee(EmployeeViewModel personChanges)
        {
            string userRoleId = (from a in context.Roles
                                 where a.Name == personChanges.Role
                                 select a.Id).ToString();

            var userRole = (from a in context.UserRoles
                            where a.UserId == personChanges.);

            Person person = new Person
            {
                FirstName = personChanges.FirstName,
                LastName = personChanges.LastName,
                Address = personChanges.Address,
                City = personChanges.City,
                PostalCode = personChanges.PostalCode,
                PhoneNumber = personChanges.PhoneNumber,
            };
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployee(id);

            if (employee != null)
            {
                var person = context.People.Find(id);
                var personUser = context.Users.Find(person.UserId);
                context.Remove(person);
                context.SaveChanges();
                context.Remove(personUser);
                context.SaveChanges();
            }
        }
    }
}
