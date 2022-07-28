using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Haulio.FarmFresh.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var customer = new Customer();
            context.Customers.Add(customer);
            Assert.AreEqual(EntityState.Added, context.Entry(customer).State);
        }
    }
}
