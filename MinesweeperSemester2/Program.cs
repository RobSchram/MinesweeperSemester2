
using LogicLayer.interfaces;
using LogicLayer.Service;
using DataLayer;
using Minesweeper.Data;
using LogicLayer.Dto;
using LogicLayer;
using DataLayer.Dao;
using Microsoft.AspNetCore.Authentication.Cookies;

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
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountDao , AccountDao>();
builder.Services.AddScoped<IGameDao , GameDao>();
builder.Services.AddScoped<IGameService , GameService>();
builder.Services.AddScoped<Game>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/LogIn"; // De route waar gebruikers naar worden omgeleid om in te loggen
        options.AccessDeniedPath = "/Account/AccessDenied"; // De route waar gebruikers naar worden omgeleid als ze geen toegang hebben
    });

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
