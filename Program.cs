using InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;
using InsurancePoliciesWebApp.Util;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(GetAllPartnersRequestHandler).Assembly
    ));
builder.Services.AddTransient<IDbConnection, SqlConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseStaticFiles();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
DatabaseMigrator.Migrate(connectionString);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Partner}/{action=Index}/{id?}");

app.Run();
