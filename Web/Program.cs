using Microsoft.Extensions.DependencyInjection;
using Web.Biblioteca.Middleware;
using Web.Biblioteca.Notification;
using Web.Interface;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel();

// Add services to the container.
builder.Services.AddControllersWithViews();


//Esta linha fez com que os campos Nullos do Modelo deixassem de ser obrigatorios
//builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.MaxModelValidationErrors = 50;
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Este Campo é Obrigatório.");
    }).AddRazorRuntimeCompilation(); ;

builder.Services.Add_DI(builder.Configuration);

var app = builder.Build();




AlertHandler.SetHttpContextAccessor(new HttpContextAccessor());


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/PageNotFound";
        await next();
    }
   
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




//https://programtown.com/how-to-create-custom-user-login-authentication-with-session-in-asp-net-core-mvc-visual-studio-2022/
//https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0
//https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0
