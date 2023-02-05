using Microsoft.EntityFrameworkCore;
using RestaurantMenuApi.Model;
using RestaurantMenuApiHaftaIki.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("RestaurantMenuDbContextConnection") ??
    throw new InvalidOperationException("Connection string 'RestaurantMenuDbContextConnection' not found.");

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRestaurantMenuItemRepository, RestaurantMenuItemRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddDbContext<RestaurantMenuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:RestaurantMenuDbContextConnection"]);

});

var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapDefaultControllerRoute();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RestaurantMenuDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
DbInitializer.Seed(app);
app.Run();
