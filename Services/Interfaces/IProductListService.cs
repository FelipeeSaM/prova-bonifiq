using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface IProductListService {
        Task<ProductList> ListProductsAsync(int qtePaginas);
    }
}
