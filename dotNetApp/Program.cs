using dotNet.DAO.Data;
using Microsoft.EntityFrameworkCore;
using dotNet.DAO.Repository.IRepository;
using dotNet.DAO.Repository;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//So,this code configures the dependency injection container to provide an instance of ApplicationDbContext when it is requested. It also specifies the SQL Server database provider and the connection string to be used for creating the ApplicationDbContext instance.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefautlConnection"))); 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
