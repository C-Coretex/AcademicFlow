using AcademicFlow.Domain.Extensions;
using AcademicFlow.Infrastructure.Extensions;
using AcademicFlow.Managers.Extensions;

var builder = WebApplication.CreateBuilder(args);

var academicFlowConnectionString = builder.Configuration.GetConnectionString("AcademicFlowConnectionString") ?? throw new InvalidOperationException("Connection string 'AcademicFlowConnectionString' not found.");
builder.Services.RegisterRepositories(academicFlowConnectionString);
builder.Services.RegisterServices();
builder.Services.RegisterManagers();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
