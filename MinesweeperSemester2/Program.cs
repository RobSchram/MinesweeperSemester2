
using LogicLayer.interfaces;
using LogicLayer.Service;
using DataLayer;
using Minesweeper.Data;
using LogicLayer.Dto;
using LogicLayer;
using DataLayer.Dao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<IFieldDao, FieldDao>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<Mines>();
builder.Services.AddScoped<CellRevealer>();
builder.Services.AddScoped<FieldGenerator>();
builder.Services.AddScoped<ICellDao , CellDao>();



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
