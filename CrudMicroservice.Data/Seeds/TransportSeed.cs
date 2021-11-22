using CrudMicroservice.Data.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMicroservice.Data.Seeds
{
    public class TransportSeed
    {
        public static async Task SeedTransportData(CrudDbContext _context)
        {
            if (!_context.Transports.AsQueryable().Any())
            {
                var transports = new List<Transport>
                {
                    new Transport
                    {
                        Id = "6189521bd065286dcfcd4cb5",
                        CarCategory = new TransportCategory()
                        {
                            CategoryName = "B"
                        },
                        CarPlate = "ВК1212ВК",
                        Color = "Black",
                        InsuaranceNumber = new Insuarance()
                        {
                            CompanyName= "Uniqa",
                            SerialNumber = "12345678"
                        },
                        Model = "328i",
                        ProducedBy = "BMW",
                        VINCode = "JH4DC4460SS000830",
                        UserId = "9017ee4a-8b32-4b74-bb8a-f0c06f515209",
                        YearOfProduction = 2013,
                    },
                    new Transport
                    {
                        CarCategory = new TransportCategory()
                        {
                            CategoryName = "B"
                        },
                        CarPlate = "ВК00001ВК",
                        Color = "White",
                        InsuaranceNumber = new Insuarance()
                        {
                            CompanyName= "Uniqa",
                            SerialNumber = "12345679"
                        },
                        Model = "A4",
                        ProducedBy = "Audi",
                        VINCode = "JW2DC4460QW001830",
                        UserId = "db4ee48b-5379-400b-93f1-d586cf6ae794",
                        YearOfProduction = 2007,
                    },
                };
                await _context.Transports.InsertManyAsync(transports);
            }
        }
    }
}
