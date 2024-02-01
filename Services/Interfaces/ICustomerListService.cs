using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface ICustomerListService {
        Task<CustomerList> ListCustomersAsync(int page);
    }
}
