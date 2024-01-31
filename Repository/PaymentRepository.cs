using ProvaPub.Models;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Repository {
    public class PaymentRepository : IPaymentRepository {
        public async Task<Order> Pix(decimal paymentValue, int customerId) {
            // lógica de pagamento para o pix
            return await Task.FromResult(new Order { Id = 1, Value = paymentValue, CustomerId = customerId, OrderDate = DateTime.Now });
        }

        public async Task<Order> CreditCard(decimal paymentValue, int customerId) {
            // lógica de pagamento para Credit Card
            return await Task.FromResult(new Order { Id = 1, Value = paymentValue, CustomerId = customerId, OrderDate = DateTime.Now });
        }

        public async Task<Order> Paypal(decimal paymentValue, int customerId) {
            // Alguma lógica de pagamento para o Paypal
            return await Task.FromResult( new Order { Id = 1, Value = paymentValue, CustomerId = customerId, OrderDate = DateTime.Now });
        }
    }
}