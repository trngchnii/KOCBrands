using api.Data;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Microsoft.Extensions.FileProviders;
using WebClient;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
// Add services to the container.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()          // Cho ph�p t?t c? c�c ngu?n (origins)
              .AllowAnyMethod()          // Cho ph�p t?t c? c�c ph??ng th?c HTTP (GET, POST, PUT, DELETE, ...)
              .AllowAnyHeader();         // Cho ph�p t?t c? c�c headers
    });
});
builder.Services.AddSingleton(payOS);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("WebClient"));
});
builder.Services.AddScoped<IProposalRepository,ProposalRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
builder.Logging.AddConsole(); // Để log ra console

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.MapAreaControllerRoute(
       name: "admin",
          areaName: "Admin",
             pattern: "Admin/{controller=Admin}/{action=Dashboard}/{id?}");

app.Run();
