using Amigos.DataAccessLayer;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Amigos;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Sección de internacionalización
        builder.Services.AddLocalization(options => { options.ResourcesPath = "Material"; });
        builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

        //Cookies de idioma
        builder.Services.Configure<RequestLocalizationOptions>(
            options =>
            {
                //Establecer el idioma por defecto (español) y añadir el soporte al resto
                var idiomas = new List<CultureInfo>
                {
                    new CultureInfo("es"),
                    new CultureInfo("en"),
                };

                options.DefaultRequestCulture = new RequestCulture("es");
                options.SupportedCultures = idiomas;
                options.SupportedUICultures = idiomas;
            }
            );

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AmigoDBContext>(options =>
      options.UseSqlite("Data Source=Amigos.db"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseRequestLocalization();

        //Cantidad de idiomas soportados
        //var idiomas = new[] { "es", "en" };

        //Establecer el idioma por defecto (español) y añadir el soporte al resto
        //var opciones_localizacion = new RequestLocalizationOptions().SetDefaultCulture(idiomas[0])
        //    .AddSupportedCultures(idiomas)
        //    .AddSupportedUICultures(idiomas);
        //app.UseRequestLocalization(opciones_localizacion);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
