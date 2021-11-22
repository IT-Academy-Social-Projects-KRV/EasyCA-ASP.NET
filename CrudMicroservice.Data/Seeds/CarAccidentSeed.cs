using CrudMicroservice.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMicroservice.Data.Seeds
{
    public class CarAccidentSeed
    {
        public static async Task SeedCarAccidentData(CrudDbContext _context)
        {
            if (!_context.CarAccidents.AsQueryable().Any())
            {
                List<Evidence> evidences = new List<Evidence>();
                Evidence evidence = new Evidence()
                {
                    PhotoSchema = "61963ec5255001ac041338ee",
                };
                evidences.Add(evidence);

                var carAccidents = new List<CarAccident>
                {
                    new CarAccident
                    {
                        SerialNumber = "00000001",
                        CourtDTG = new DateTime(),
                        SideOfAccident = new SideCA()
                        {
                            DriverLicenseSerial = "QWE123456",
                            Email = "pinkevych@gmail.com",
                            TransportId = "6189521bd065286dcfcd4cb5"
                        },
                        TrafficRuleId = "321",
                        Evidences = evidences,
                        AccidentCircumstances = "Some circumstanse!",
                        Address = new AddressOfAccident()
                        {
                            City = "Rivne",
                            CoordinatesOfLongitude = "123",
                            CrossStreet = "Soborna",
                            District = "Rivne",
                            IsInCity = true,
                            IsIntersection = false,
                            Street = "Soborna",
                            CoordinatesOfLatitude = "123",
                        },
                        DriverExplanation = "nu vot tak poluchilos, sorry",
                        IsDocumentTakenOff = false,
                        RegistrationDateTime = new DateTime(),
                        Witnesses = new List<Witness>(),
                        InspectorId = "52774ed6-26c9-41c7-accd-5445144d1241",
                    }
                };
                await _context.CarAccidents.InsertManyAsync(carAccidents);
            }
        }
    }
}
