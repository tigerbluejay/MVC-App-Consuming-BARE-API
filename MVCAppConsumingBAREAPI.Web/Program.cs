using Microsoft.AspNetCore.Authentication.Cookies;
using MVCAppConsumingBAREAPI.Utilities.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/******* Add AutoMapper *******/
builder.Services.AddAutoMapper(typeof(Mapping));

/******* Add HttpClient on Services *******/
/******* Register Services for Dependency Injection *****/

//builder.Services.AddHttpClient<IVillaService, VillaService>();
//builder.Services.AddScoped<IVillaService, VillaService>();

//builder.Services.AddHttpClient<IVillaNumberService, VillaNumberService>();
//builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();

//builder.Services.AddHttpClient<IAuthService, AuthService>();
//builder.Services.AddScoped<IAuthService, AuthService>();

/******* Add HttpContextAccessor in _Layout *******/
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/******* Add Authentication and Cache *******/
/* We need to add this to pass the token in authentication, to save it for all subsequent requests
 * This way the API will know the user is authenticated - for this we save the token into a session */
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.LoginPath = "/Auth/Login"; // // here we code the right path
            options.AccessDeniedPath = "/Auth/AccessDenied"; // here we code the right path
            options.SlidingExpiration = true;
        });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
