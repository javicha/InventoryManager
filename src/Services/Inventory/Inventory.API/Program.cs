using Application.Middleware;
using Inventory.API.Extensions;
using Inventory.Application;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Persistence;
using MassTransit;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//WebApplicationBuilder returned by WebApplication.CreateBuilder(args) exposes Configuration and Environment properties
ConfigurationManager configuration = builder.Configuration; 

// Add services to the container.

builder.Services.AddControllers();


// Add MassTransit and configure it with RabbitMQ connection
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //API information and description
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Inventory",
        Description = "Goal Systems - Inventory Web API",
        Contact = new OpenApiContact
        {
            Name = "Javier Val",
            Url = new Uri("https://www.linkedin.com/in/javier-val-lista-ing-software/")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
   
    //We configure Swagger to use the documentation XML file
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Register all the Application and Infrastructure layers services 
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();
app.PopulateDatabase<InventoryContext>((context, services) =>
{
    var logger = services.GetService<ILogger<InventoryContextSeed>>();
    InventoryContextSeed.SeedAsync(context, logger).Wait();
}
);

// Configure the HTTP request pipeline.
app.UseMiddleware<JwtMiddleware>(); //Authentication middleware in order to securize API
app.UseMiddleware<ExceptionHandlingMiddleware>(); //Global handling exceptions
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Manger API - v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
