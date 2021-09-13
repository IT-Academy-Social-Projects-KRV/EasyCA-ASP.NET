using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProtocolService.Data.Entities;

namespace ProtocolService.Data.Seeds
{
    public class CircumstanceAppSeed
    {
        public static async Task SeedCircumstances(ProtocolDbContext _context)
        {
            if (!_context.Circumstances.AsQueryable().Any())
            {
                var listOfCircumstances = new List<Circumstance>
                {
                    new Circumstance
                    {
                        CircumstanceName = "During the staying on the parking lot/halting point",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While departing from parking lot/while door opening",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning into the parking lot",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While departing from parking lot, located on private plots, or from byroads/railway",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning into parking lot, located on private plots, or into byroad/railway",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning into roundabout",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While driving by roundabout",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "Collision with rear-part of another vehicle while driving in the same direction by the same lane (rear-end collision)",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "Collision while driving the same direction but another lane",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While changing the lane",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While overtaking",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning right",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning left",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While driving in opposite direction",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While turning into the lane, designated for traffic moving in opposite directions",
                    },
                    new Circumstance
                    {
                        CircumstanceName = "While driving from the right side of weaving section",
                    },new Circumstance
                    {
                        CircumstanceName = "As result of ignoring priority traffic sign or stop-light",
                    },
                };
                await _context.Circumstances.InsertManyAsync(listOfCircumstances);
            }
        }
    }
}
