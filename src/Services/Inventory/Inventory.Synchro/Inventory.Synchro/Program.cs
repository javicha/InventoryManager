using Coravel;
using Coravel.Scheduling.Schedule.Interfaces;
using Inventory.Application;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.ScheduledJobs;
using Inventory.Synchro.Extensions;

var builder = WebApplication.CreateBuilder(args);

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

using (var scope = app.Services.CreateScope())
{
    //2. Get the instance of InventoryContext in our services layer
    var services = scope.ServiceProvider;
    services.UseScheduler(scheduler =>
    {
        scheduler.Schedule<ProductsExpiredJob>()
            .Daily()
            .RunOnceAtStart();
    })
    .LogScheduledTaskProgress(services.GetService<ILogger<IScheduler>>())
    .OnError((exception) => Console.WriteLine(exception));
}

app.Run();
