using Microsoft.EntityFrameworkCore;
using KurumsalEgitimSitesi.Data;

var builder = WebApplication.CreateBuilder(args);

// Railway PORT desteği
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL + Entity Framework Core
// Railway environment variable desteği — tüm olası isimleri kontrol et
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") 
    ?? Environment.GetEnvironmentVariable("DATABASE_PRIVATE_URL")
    ?? Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL")
    ?? Environment.GetEnvironmentVariable("PGHOST"); // Railway individual vars
string connectionString;

// Debug: Tüm DB ile ilgili env var'ları logla
Console.WriteLine("[DB] Environment variables kontrol ediliyor...");
foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
{
    var key = env.Key.ToString()!;
    if (key.Contains("DATABASE") || key.Contains("PG") || key.Contains("POSTGRES"))
        Console.WriteLine($"[DB]   {key} = {(env.Value?.ToString()?.Length > 20 ? env.Value?.ToString()?[..20] + "..." : env.Value)}");
}

if (!string.IsNullOrEmpty(databaseUrl))
{
    if (databaseUrl.StartsWith("postgresql://") || databaseUrl.StartsWith("postgres://"))
    {
        Console.WriteLine($"[DB] DATABASE_URL bulundu, parse ediliyor...");
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
        Console.WriteLine($"[DB] Host={uri.Host}, Port={uri.Port}, Database={uri.AbsolutePath.TrimStart('/')}");
    }
    else
    {
        // PGHOST bulunmuşsa, bireysel Railway Postgres env var'larını kullan
        var pgHost = Environment.GetEnvironmentVariable("PGHOST");
        var pgPort = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
        var pgDb = Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway";
        var pgUser = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
        var pgPass = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "";
        connectionString = $"Host={pgHost};Port={pgPort};Database={pgDb};Username={pgUser};Password={pgPass};SSL Mode=Require;Trust Server Certificate=true";
        Console.WriteLine($"[DB] Individual PG vars kullanılıyor: Host={pgHost}, Port={pgPort}, Database={pgDb}");
    }
}
else
{
    Console.WriteLine("[DB] Hiçbir DATABASE env var bulunamadı, appsettings.json kullanılıyor.");
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
