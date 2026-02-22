using Microsoft.EntityFrameworkCore;
using KurumsalEgitimSitesi.Data;

var builder = WebApplication.CreateBuilder(args);

// Railway PORT desteği
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL + Entity Framework Core — Railway desteği
// Debug: Tüm DB ile ilgili env var'ları logla
Console.WriteLine("[DB] === Environment Variables ===");
foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
{
    var key = env.Key.ToString()!;
    if (key.Contains("DATABASE", StringComparison.OrdinalIgnoreCase) || 
        key.Contains("PG", StringComparison.OrdinalIgnoreCase) || 
        key.Contains("POSTGRES", StringComparison.OrdinalIgnoreCase))
    {
        var val = env.Value?.ToString() ?? "(null)";
        Console.WriteLine($"[DB]   {key} = {(val.Length > 30 ? val[..30] + "..." : val)}");
    }
}

string connectionString;
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var databasePrivateUrl = Environment.GetEnvironmentVariable("DATABASE_PRIVATE_URL");
var databasePublicUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL");
var pgHost = Environment.GetEnvironmentVariable("PGHOST");

Console.WriteLine($"[DB] DATABASE_URL: {(databaseUrl != null ? "SET" : "NULL")}");
Console.WriteLine($"[DB] DATABASE_PRIVATE_URL: {(databasePrivateUrl != null ? "SET" : "NULL")}");
Console.WriteLine($"[DB] DATABASE_PUBLIC_URL: {(databasePublicUrl != null ? "SET" : "NULL")}");
Console.WriteLine($"[DB] PGHOST: {(pgHost != null ? "SET" : "NULL")}");

// Boş string'leri de null olarak ele al
var chosenUrl = new[] { databaseUrl, databasePrivateUrl, databasePublicUrl }
    .FirstOrDefault(u => !string.IsNullOrWhiteSpace(u));

Console.WriteLine($"[DB] chosenUrl: {(chosenUrl != null ? $"'{chosenUrl[..Math.Min(40, chosenUrl.Length)]}...'" : "NULL")}");

if (!string.IsNullOrWhiteSpace(chosenUrl))
{
    Console.WriteLine($"[DB] URL bulundu ({chosenUrl.Length} karakter), parse ediliyor...");
    try
    {
        var uri = new Uri(chosenUrl);
        var userInfo = uri.UserInfo.Split(':');
        connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
        Console.WriteLine($"[DB] Bağlantı: Host={uri.Host}, Port={uri.Port}, Database={uri.AbsolutePath.TrimStart('/')}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB] URL parse hatası: {ex.Message}");
        Console.WriteLine($"[DB] URL ilk 40 karakter: {chosenUrl[..Math.Min(40, chosenUrl.Length)]}");
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    }
}
else if (!string.IsNullOrEmpty(pgHost))
{
    var pgPort = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
    var pgDb = Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway";
    var pgUser = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
    var pgPass = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "";
    connectionString = $"Host={pgHost};Port={pgPort};Database={pgDb};Username={pgUser};Password={pgPass};SSL Mode=Require;Trust Server Certificate=true";
    Console.WriteLine($"[DB] PG vars: Host={pgHost}, Port={pgPort}, Database={pgDb}");
}
else
{
    Console.WriteLine("[DB] Hiçbir Railway DB env var bulunamadı! appsettings.json fallback.");
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
}
Console.WriteLine("[DB] === Bağlantı hazır ===");

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
