using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface IPaymentService {
        Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
    }
}
