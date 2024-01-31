using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface IPaymentRepository {
        Task<Order> Pix(decimal paymentValue, int customerId);
        Task<Order> CreditCard(decimal paymentValue, int customerId);
        Task<Order> Paypal(decimal paymentValue, int customerId);
    }
}
