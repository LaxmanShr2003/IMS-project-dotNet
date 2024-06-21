using IMS.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IMS.web.Models;
using IMS.infrastructure;
using IMS.infrastructure.Repository.CRUD;
using IMS.infrastructure.Repository;
using IMS.infrastructure.IRepository;
using IMS.infrastructure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages(); 

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddIdentity<ApplicationIdentity, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddDbContext<ImsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    e => e.MigrationsAssembly("IMS.web")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));
builder.Services.AddTransient<IRawSqlRepository, RawSqlRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
