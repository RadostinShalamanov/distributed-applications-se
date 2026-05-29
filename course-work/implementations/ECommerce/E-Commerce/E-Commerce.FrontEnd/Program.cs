using E_Commerce.FrontEnd.Services;

namespace E_Commerce.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<ProductApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7087/");
            });
            builder.Services.AddHttpClient<OrderApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7087/");
            });

            builder.Services.AddHttpClient<UserApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7087/");
            });

            builder.Services.AddHttpClient<CategoryApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7087/");
            });

            // Auth API client for login
            builder.Services.AddHttpClient<AuthApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7087/");
            });

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession();
            // provide access to HttpContext for services that need session/token
            builder.Services.AddHttpContextAccessor();

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
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
