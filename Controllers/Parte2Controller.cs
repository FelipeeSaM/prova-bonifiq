using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		/// <summary>
		/// Precisamos fazer algumas alterações:
		/// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
		/// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
		/// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
		/// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
		/// 
		/// </summary>
		/// 

		private readonly IProductListService _productListService;
		private readonly ICustomerListService _customerListService;

		public Parte2Controller(IProductListService productList, ICustomerListService customerList)
		{
			_productListService = productList;
			_customerListService = customerList;
		}
	
		[HttpGet("products")]
		public Task<ProductList> ListProductsAsync(int page)
		{
			return _productListService.ListProductsAsync(page);
		}

		[HttpGet("customers")]
		public async Task<CustomerList> ListCustomersAsync(int page)
		{
			return await _customerListService.ListCustomersAsync(page);
		}
	}
}
