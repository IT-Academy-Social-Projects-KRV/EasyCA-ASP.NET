using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Data.Seeds
{
    public class CircumstanceAppSeed
    {
        public static async Task SeedCircumstances(CrudDbContext _context)
        {
            if (!_context.Circumstances.AsQueryable().Any())
            {
                var listOfCircumstances = new List<Circumstance>
                {
                    new Circumstance
                    {
                        CircumstanceId = 1,
                        CircumstanceName = "During the staying on the parking lot/halting point",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 2,
                        CircumstanceName = "While departing from parking lot/while door opening",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 3,
                        CircumstanceName = "While turning into the parking lot",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 4,
                        CircumstanceName = "While departing from parking lot, located on private plots, or from byroads/railway",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 5,
                        CircumstanceName = "While turning into parking lot, located on private plots, or into byroad/railway",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 6,
                        CircumstanceName = "While turning into roundabout",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 7,
                        CircumstanceName = "While driving by roundabout",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 8,
                        CircumstanceName = "Collision with rear-part of another vehicle while driving in the same direction by the same lane (rear-end collision)",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 9,
                        CircumstanceName = "Collision while driving the same direction but another lane",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 10,
                        CircumstanceName = "While changing the lane",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 11,
                        CircumstanceName = "While overtaking",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 12,
                        CircumstanceName = "While turning right",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 13,
                        CircumstanceName = "While turning left",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 14,
                        CircumstanceName = "While driving in opposite direction",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 15,
                        CircumstanceName = "While turning into the lane, designated for traffic moving in opposite directions",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 16,
                        CircumstanceName = "While driving from the right side of weaving section",
                    },
                    new Circumstance
                    {
                        CircumstanceId = 17,
                        CircumstanceName = "As result of ignoring priority traffic sign or stop-light",
                    },
                };
                await _context.Circumstances.InsertManyAsync(listOfCircumstances);
            }
        }
    }
}
