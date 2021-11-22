using CrudMicroservice.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMicroservice.Data.Seeds
{
    public class EuroProtocolSeed
    {
        public static async Task SeedCarAccidentData(CrudDbContext _context)
        {
            if (!_context.EuroProtocols.AsQueryable().Any())
            {
                List<Evidence> evidences = new List<Evidence>();
                Evidence evidence = new Evidence()
                {
                    PhotoSchema = "61963ec5255001ac041338ee",
                };
                evidences.Add(evidence);

                var euroProtocols = new List<EuroProtocol>
                {
                    new EuroProtocol
                    {
                        SerialNumber = "00000001",
                        SideA = new Side()
                        {
                            DriverLicenseSerial = "QWE123456",
                            Email = "pinkevych@gmail.com",
                            TransportId = "6189521bd065286dcfcd4cb5",
                            Circumstances = new List<int>(){
                                3
                            },
                            Damage = "front",
                            Evidences = evidences,
                            IsGulty = false
                        },
                        SideB = new Side()
                        {
                            DriverLicenseSerial = "QQQ123456",
                            Email = "okorniichuk03@gmail.com",
                            TransportId = "618b9e23bb521e2131329d0a",
                            Circumstances = new List<int>(){
                                4,3
                            },
                            Damage = "side",
                            Evidences = evidences,
                            IsGulty = true
                        },
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
                        RegistrationDateTime = new DateTime(),
                        Witnesses = new List<Witness>(),
                        IsClosed = true,
                    }
                };
                await _context.EuroProtocols.InsertManyAsync(euroProtocols);
            }
        }
    }
}
