using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordring.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(
            OrderContext orderContext,
            ILoggerFactory loggerFactory,
            int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                orderContext.Database.Migrate();
                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                retryForAvailability++;
                var log = loggerFactory.CreateLogger<OrderContextSeed>();
                log.LogError(exception.Message);
                await SeedAsync(orderContext, loggerFactory, retryForAvailability);

            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order(){UserName = "pnawaz", FirstName = "Parvez", LastName = "Nawaz", EmailAddress = "parvez.nawaz@gmail.com", AddressLine = "88 Radford drive Ajax", Country = "Canada"}
            };
        }
    }
}
