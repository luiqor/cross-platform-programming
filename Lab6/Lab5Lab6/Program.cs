
using Lab5Lab6.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Auth0UserService>();
builder.Services.AddScoped<IExternalApiService, Lab6Service>();
builder.Services.AddHttpClient<IExternalApiService, Lab6Service>(client => client.BaseAddress = new Uri("http://localhost:5050"));
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "/Lab6/Address",
    pattern: "{controller=Lab6}/{action=Address}/{id?}");

app.MapControllerRoute(
    name: "/Lab6/Order",
    pattern: "{controller=Lab6}/{action=Order}/{id?}");

app.MapControllerRoute(
    name: "/Lab6/Product",
    pattern: "{controller=Lab6}/{action=Product}/{id?}");

app.MapDefaultControllerRoute();

app.Run();
