using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Generics;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class ProductService : IProductListService
	{
		TestDbContext _ctx;

        private readonly IGenericProcessLists<Product> _ctxProduct;
        

		public ProductService(TestDbContext ctx, IGenericProcessLists<Product> ctxProduct)
		{
			_ctx = ctx;
            _ctxProduct = ctxProduct;
		}

        public async Task<ProductList> ListProductsAsync(int page) {
        
            var products = await _ctxProduct.ListEntitiesAsync(page);

            ProductList list = new ProductList {
                HasNext = false,
                TotalCount = products.Count(),
                Products = products
            };

            return list;
        }

    }
}
