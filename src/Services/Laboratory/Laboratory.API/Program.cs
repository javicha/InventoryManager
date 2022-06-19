using EventBus.Messages.Common;
using Laboratory.API.EventBusConsumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
//WebApplicationBuilder returned by WebApplication.CreateBuilder(args) exposes Configuration and Environment properties
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Add MassTransit and configure it with RabbitMQ connection
builder.Services.AddMassTransit(config => {

    // Declare the events consumers
    config.AddConsumer<ProductExpiredConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);

        //Providing the queues to which each consumer subscribes
        cfg.ReceiveEndpoint(EventBusConstants.ProductExpiredQueue, c =>
        {
            c.ConfigureConsumer<ProductExpiredConsumer>(ctx);
        });
    });
});

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
