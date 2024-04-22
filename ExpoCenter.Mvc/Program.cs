using ExpoCenter.Dominio.Interfaces;
using ExpoCenter.Mvc.Data;
using ExpoCenter.Repositorios.Http;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("IdentityConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var expoCenterConnectionString = builder.Configuration.GetConnectionString("ExpoCenterConnection");
builder.Services.AddDbContext<ExpoCenterDbContext>(options =>
    options
    .UseLazyLoadingProxies()
    .UseSqlServer(expoCenterConnectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddAuthorization(o => o.AddPolicy("ParticipantesExcluir", ParticipantesExcluirPolicy));

void ParticipantesExcluirPolicy(AuthorizationPolicyBuilder builder)
{
    builder.RequireAssertion(h => h.User.IsInRole("Gerente")|| h.User.HasClaim("Participantes", "Excluir"));
}

builder.Services.AddControllersWithViews();

builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddHttpClient<IPagamentoRepositorio, PagamentoRepositorio>( c => 
{
    c.BaseAddress = new Uri(builder.Configuration.GetSection("Endpoints:ApiExpoCenter").Value.TrimEnd('/') + "/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
