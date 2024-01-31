using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces {
    public interface IProductListService {
        Task<List<ProductList>> ListProductsAsync(int qtePaginas);
    }
}
