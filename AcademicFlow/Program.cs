using AcademicFlow.Domain.Extensions;
using AcademicFlow.Filters;
using AcademicFlow.Infrastructure.Extensions;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Extensions;
using AcademicFlow.Managers.Managers;

var builder = WebApplication.CreateBuilder(args);

var academicFlowConnectionString = builder.Configuration.GetConnectionString("AcademicFlowConnectionString") ?? throw new InvalidOperationException("Connection string 'AcademicFlowConnectionString' not found.");
builder.Services.RegisterRepositories(academicFlowConnectionString);
builder.Services.RegisterServices();
builder.Services.RegisterManagers();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AuthorizeUser.ServiceScopeFactory = app.Services.CreateScope;

//add session and authentication filter 
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
