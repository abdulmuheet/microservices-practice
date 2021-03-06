using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["CacheSettings:ConnectionString"];    
});

// General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(StartupBase));

// Grpc Configuration
//builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            //(o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
//builder.Services.AddScoped<DiscountGrpcService>();

// MassTransit-RabbitMQ Configuration
//builder.Services.AddMassTransit(config => {
//    config.UsingRabbitMq((ctx, cfg) => {
//        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
//    });
//});
//builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
