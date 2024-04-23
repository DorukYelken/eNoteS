// Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Servislerin eklenmesi
builder.Services.AddControllers();

var app = builder.Build();


// Geliştirme ortamında hata sayfasının kullanılması
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Routing tanımlamaları
app.UseRouting();

// Giriş sayfası için endpoint tanımlaması
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/login", async context =>
    {
        await context.Response.WriteAsync(await System.IO.File.ReadAllTextAsync("index.html"));
    });

    endpoints.MapGet("/mainPage", async context =>
    {
        await context.Response.WriteAsync(await System.IO.File.ReadAllTextAsync("mainPage.html"));
    });

    endpoints.MapGet("/profil", async context =>
    {
        await context.Response.WriteAsync(await System.IO.File.ReadAllTextAsync("profil.html"));
    });


   



    endpoints.MapPost("/login", async context =>
    {
        // Formdan kullanıcı adı, şifre ve şifre tekrarını al
        var username = context.Request.Form["username"];
        var password = context.Request.Form["password"];
        

        // Şifrenin tekrarı doğrulanıyor
        if (password == "root" && username == "admin")
        {
            context.Response.StatusCode = 400; // Bad Request
            context.Response.Redirect("/mainPage");
            return;
        }

        // Kullanıcıyı veritabanına kaydetme işlemi burada gerçekleştirilmeli

        // Başarılı kayıt mesajı gönder
        context.Response.StatusCode = 200; // OK
        await context.Response.WriteAsync("FALSE");
    });




    // Diğer endpoint tanımlamaları buraya gelebilir
    endpoints.MapGet("/register", async context =>
    {
        await context.Response.WriteAsync(await System.IO.File.ReadAllTextAsync("register.html"));
    });

    endpoints.MapPost("/register", async context =>
    {
        // Formdan kullanıcı adı, şifre ve şifre tekrarını al
        var username = context.Request.Form["username"];
        var password = context.Request.Form["password"];
        var confirmPassword = context.Request.Form["confirm-password"];

        // Şifrenin tekrarı doğrulanıyor
        if (password != confirmPassword)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Şifreler uyuşmuyor.");
            return;
        }

        // Kullanıcıyı veritabanına kaydetme işlemi burada gerçekleştirilmeli

        // Başarılı kayıt mesajı gönder
        context.Response.StatusCode = 200; // OK
        await context.Response.WriteAsync("Kayıt işlemi başarılı");
    });

});

// Uygulamanın çalıştırılması
app.Run();

