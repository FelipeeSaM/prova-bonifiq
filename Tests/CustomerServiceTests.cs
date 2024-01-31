using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private DbContextOptions<TestDbContext> _options;

        [SetUp]
        public void Setup() {
            _options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "CanPurchaseTestDatabase")
                .Options;
        }

        [TestCase(1, 20)]
        public async Task CanPurchaseMethodValidation_ReturnsTrue(int customerId, int purchasedValue) {

            using(var context = new TestDbContext(_options)) {
                context.Customers.Add(new Customer { Id = customerId, Name = "leo" });
                context.SaveChanges();

                var customerService = new CustomerService(context, null);

                var result = await customerService.CanPurchase(customerId, purchasedValue);

                Assert.That(result, Is.True);
            }
        }


        [TestCase(1)]
        public async Task IsCustomerRegistered_CustomerExists_ReturnsTrue(int customerId) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                var result = await customerService.IsCustomerRegistered(customerId);

                Assert.That(result, Is.True);
            }
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task IsCustomerRegistered_CustomerDoesNotExist_ThrowsInvalidOperationException(int customerId) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                Assert.ThrowsAsync<InvalidOperationException>(() => customerService.IsCustomerRegistered(customerId));
            }
        }

        [TestCase(1, 50)]
        [TestCase(1, 100)]
        public void IsValidInputs_ValidInputs_ReturnsTrue(int customerId, int puchaseValue) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                var result = customerService.IsValidInputs(customerId, puchaseValue);

                Assert.That(result, Is.True);
            }
        }

        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        [TestCase(0, -1)]
        public void IsValidInputs_InvalidInputs_ThrowsArgumentOutOfRangeException(int customerId, int puchaseValue) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                Assert.Throws<ArgumentOutOfRangeException>(() => customerService.IsValidInputs(customerId, puchaseValue));
            }
        }

        [TestCase(1)]
        public async Task CanDoNewPurchase_NoOrdersInLastMonth_ReturnsTrue(int customerId) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                var result = await customerService.CanDoNewPurchase(customerId);

                Assert.That(result, Is.True);
            }
        }

        [TestCase(1, 50)]
        [TestCase(1, 76)]
        public async Task IsFirstBoughtAndCanBuy_CustomerHasNotBoughtBeforeAndPurchaseValueValid_ReturnsTrue(int customerId, int purchaseValue) {

            using(var context = new TestDbContext(_options)) {
                var customerService = new CustomerService(context, null);

                var result = await customerService.IsFirstBoughtAndCanBuy(customerId, purchaseValue);

                Assert.That(result, Is.True);
            }
        }

    }
}
