using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Generics;
using ProvaPub.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerListService
    {
        TestDbContext _ctx;
        private readonly IGenericProcessLists<Customer> _ctxCustomer;
        private bool isValid;

        public CustomerService(TestDbContext ctx, IGenericProcessLists<Customer> ctxCustomer)
        {
            _ctx = ctx;
            _ctxCustomer = ctxCustomer;
        }

        public CustomerService()
        {
            
        }

        //public CustomerList ListCustomers(int page)
        //{
        //    return new CustomerList() { HasNext = false, TotalCount = 10, Customers = _ctx.Customers.ToList() };
        //}

        //public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        //{
        //    if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

        //    if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

        //    //Business Rule: Non registered Customers cannot purchase
        //    var customer = await _ctx.Customers.FindAsync(customerId);
        //    if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

        //    //Business Rule: A customer can purchase only a single time per month
        //    var baseDate = DateTime.UtcNow.AddMonths(-1);
        //    var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
        //    if (ordersInThisMonth > 0)
        //        return false;

        //    //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
        //    var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
        //    if (haveBoughtBefore == 0 && purchaseValue > 100)
        //        return false;

        //    return true;
        //}

        public async Task<List<CustomerList>> ListCustomersAsync(int page) {
            List<CustomerList> listToRender = new List<CustomerList>();
            var customers = await _ctxCustomer.ListEntitiesAsync(page);

            CustomerList list = new CustomerList {
                HasNext = false,
                TotalCount = customers.Count(),
                Customers = customers
            };

            listToRender.Add(list);

            return listToRender;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue) {
            
            isValid = IsValidInputs(customerId, purchaseValue) && await IsCustomerRegistered(customerId) &&
                await CanDoNewPurchase(customerId) && await IsFirstBoughtAndCanBuy(customerId, purchaseValue);

            return isValid;
        }

        public async Task<bool> IsCustomerRegistered(int customerId) {
            try {
                var customer = await _ctx.Customers.SingleOrDefaultAsync(c => c.Id.Equals(customerId));
                return customer == null ? throw new InvalidOperationException($"Customer Id {customerId} does not exists") : true;
            }
            catch(Exception) {

                throw;
            }
        }

        public bool IsValidInputs(int customerId, decimal purchaseValue) {
            return customerId <= 0 ? throw new ArgumentOutOfRangeException(nameof(customerId)) : purchaseValue <= 0 ?
                throw new ArgumentOutOfRangeException(nameof(purchaseValue)) : true;
        }

        public async Task<bool> CanDoNewPurchase(int customerId) {
            try {
                // Aqui aparentemente vai sempre ser true, porque não existe nenhum dado na tabela Orders.
                var baseDate = DateTime.UtcNow.AddMonths(-1);
                var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
                return ordersInThisMonth == 0 ? true : false;
            }
            catch(Exception) {

                throw;
            }
        }

        public async Task<bool> IsFirstBoughtAndCanBuy(int customerId, decimal purchaseValue) {
            try {
                var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
                return haveBoughtBefore == 0 && purchaseValue <= 100 ? true : false;
            }
            catch(Exception) {

                throw;
            }

        }
    }
}