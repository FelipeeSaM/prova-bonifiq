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

        public async Task<List<ProductList>> ListProductsAsync(int page) {

            List<ProductList> listToRender = new List<ProductList>();
            var products = await _ctxProduct.ListEntitiesAsync(page);
            // ou utilizando de lógica, poderia pegar uma lista de products utilizando o GetRange(indice inicial, qte a exibir)
            // Pessoalmente prefiro a repetição de código aqui, pois podemos adicionar regras de negócios diferentes entre
            // Product e Customer.

            ProductList list = new ProductList {
                HasNext = false,
                TotalCount = products.Count(),
                Products = products
            };

            listToRender.Add(list);

            return listToRender;
        }

    }
}
