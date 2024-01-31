using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService : IPaymentService
	{

		private readonly IPaymentRepository _payment;

        public OrderService(IPaymentRepository payment)
        {
			_payment = payment;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{

            try {
                switch(paymentMethod.ToLower()) {
                    case "pix":
                        return await _payment.Pix(paymentValue, customerId);
                    case "creditcard":
                        return await _payment.CreditCard(paymentValue, customerId);
                    case "paypal":
                        return await _payment.Paypal(paymentValue, customerId);
                    default:
                        return await Task.FromResult(new Order() { Value = paymentValue });
                }
            }
            catch(Exception ex) {
                throw new ArgumentException($"Erro ao processar o pagamento: {ex.Message}");
            }

		}

    }
}