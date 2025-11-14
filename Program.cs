using tl2_tp8_2025_carlitos0707.Interfaces;
using tl2_tp8_2025_carlitos0707.Repositorios;
using tl2_tp8_2025_carlitos0707.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// Habilitar servicios de sesiones
builder.Services.AddSession(options =>
                                { 
                                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
                                    options.Cookie.HttpOnly = true; // Solo accesible desde HTTP, no JavaScript
                                    options.Cookie.IsEssential = true; // Necesario incluso si el usuario no acepta cookies
                                });



builder.Services.AddScoped<IProductoRepository,ProductoRepository>();
builder.Services.AddScoped<IPresupuestoRepository,PresupuestoRepository>();
builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
