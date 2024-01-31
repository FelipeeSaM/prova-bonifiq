using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Generics;
using ProvaPub.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* S� um aviso, j� que n�s n�o tivemos um contato pr�vio:
 * No formul�rio que eu vou responder, o e-mail do gmail � "arinha.botelho@gmail.com",
 * mas sou eu mesmo, foi uma longa hist�ria :-P
 * Ali�s, eu estou enviando tudo (menos a parte1Controller) em apenas um commit, porque eu
 * n�o sabia se podia ir dando os commits. Enfim, espero que possamos conversar posteriormente!!! */

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RandomService>();
builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

builder.Services.AddScoped<IProductListService, ProductService >();
builder.Services.AddScoped<ICustomerListService, CustomerService >();
builder.Services.AddScoped<IPaymentService, OrderService >(); // O nome OrderService desmantela o padr�o Interface/Service, mas preferi manter intocado o nome da clase que j� veio.
builder.Services.AddScoped<IPaymentRepository, PaymentRepository >();
builder.Services.AddScoped(typeof(IGenericProcessLists<>), typeof(GenericProcessLists<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
