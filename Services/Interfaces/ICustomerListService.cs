using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface ICustomerListService {
        Task<List<CustomerList>> ListCustomersAsync(int page);
    }
}
