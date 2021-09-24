using CrudMicroservice.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMicroservice.Data.Seeds
{
    public class PersonalDataSeed
    {
        public static async Task SeedPersonalData(CrudDbContext _context)
        {
            if (!_context.PersonalDatas.AsQueryable().Any())
            {
                var personalsData = new List<PersonalData>
                {
                    new PersonalData
                    {
                        IPN = "3862124565",
                        JobPosition = "Programmer",
                        ServiceNumber = "0",
                        Address = new Address()
                        {
                            Country = "UA",
                            City = "Rivne",
                            Region = "Rivne",
                            Street = "Soborna",
                            District = "Motorway",
                            Building="4",
                            Appartament=99,
                            PostalCode = "12345"
                        },
                        BirthDay = DateTime.Today,
                        UserCars = null,
                        UserDriverLicense = null
                    },
                };
                await _context.PersonalDatas.InsertManyAsync(personalsData);
            }
        }
    }
}
