using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.ViewModels;

namespace Dispatch_system.Services
{
    public class SQLAccountService : IAccountService
    {
        private readonly ApplicationDbContext dbContext;

        public SQLAccountService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PersonDataViewModel GetPersonalData(int personId)
        {
            var personalData = (from people in dbContext.People
                                join addresses in dbContext.UserAddresses on people.PersonId equals addresses.PersonId
                                where people.PersonId == personId
                                select new PersonDataViewModel
                                {
                                    PersonId = people.PersonId,
                                    FirstName = people.FirstName,
                                    LastName = people.LastName,
                                    PhoneNumber = people.PhoneNumber,
                                    StreetName = addresses.StreetName,
                                    BlockNumber = addresses.BlockNumber,
                                    FlatNumber = addresses.FlatNumber,
                                    PostalCode = addresses.PostalCode,
                                    City = addresses.City
                                });

            return personalData.First();
        }

        public int? GetPersonId(string email)
        {
            var query = (from user in dbContext.Users
                            join person in dbContext.People on user.Id equals person.UserId
                            where user.Email == email
                            select new
                            {
                                person.PersonId
                            }).FirstOrDefault();

            int? personId = query.PersonId;
            return personId;
        }
    }
}
