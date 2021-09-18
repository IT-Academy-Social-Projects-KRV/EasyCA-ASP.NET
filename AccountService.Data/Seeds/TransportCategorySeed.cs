using AccountService.Data.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Data.Seeds
{
    public class TransportCategorySeed
    {
        public static async Task SeedTransportCategoryData(ApplicationDbContext _context)
        {
            if (!_context.TransportCategories.AsQueryable().Any())
            {
                var transportCategories = new List<TransportCategory>
                {
                    new TransportCategory
                    {
                        CategoryName = "A1",
                    },
                    new TransportCategory
                    {
                        CategoryName = "A",
                    },
                    new TransportCategory
                    {
                        CategoryName = "В1",
                    },
                    new TransportCategory
                    {
                        CategoryName = "B",
                    },
                    new TransportCategory
                    {
                        CategoryName = "C1",
                    },
                    new TransportCategory
                    {
                        CategoryName = "C",
                    },
                    new TransportCategory
                    {
                        CategoryName = "D1",
                    },
                    new TransportCategory
                    {
                        CategoryName = "D",
                    },
                    new TransportCategory
                    {
                        CategoryName = "T",
                    },
                    new TransportCategory
                    {
                        CategoryName = "BE",
                    },
                    new TransportCategory
                    {
                        CategoryName = "C1E",
                    },
                    new TransportCategory
                    {
                        CategoryName = "CE",
                    },
                    new TransportCategory
                    {
                        CategoryName = "D1E",
                    },
                    new TransportCategory
                    {
                        CategoryName = "DE",
                    }
                };
                await _context.TransportCategories.InsertManyAsync(transportCategories);
            }
        }
    }
}
