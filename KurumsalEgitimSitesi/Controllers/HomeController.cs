using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KurumsalEgitimSitesi.Models;
using KurumsalEgitimSitesi.Data;
using System.Net;
using System.Net.Mail;

namespace KurumsalEgitimSitesi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
    {
        _logger = logger;
        _configuration = configuration;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.FeaturedTrainings = await _context.Trainings
            .Include(t => t.Category)
            .Where(t => t.IsActive && t.IsFeatured)
            .OrderBy(t => t.SortOrder)
            .Take(6)
            .ToListAsync();
        ViewBag.UpcomingEvents = await _context.Events
            .Where(e => e.IsActive && e.StartDate >= DateTime.UtcNow)
            .OrderBy(e => e.StartDate)
            .Take(3)
            .ToListAsync();
        ViewBag.References = await _context.References
            .Where(r => r.IsActive)
            .OrderBy(r => r.SortOrder)
            .ToListAsync();
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> TrainingCatalog()
    {
        var categories = await _context.Categories
            .Include(c => c.Trainings.Where(t => t.IsActive))
            .Where(c => c.IsActive)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> TrainingDetail(int id)
    {
        var training = await _context.Trainings
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
        if (training == null) return NotFound();

        ViewBag.RelatedTrainings = await _context.Trainings
            .Where(t => t.CategoryId == training.CategoryId && t.Id != training.Id && t.IsActive)
            .Take(3)
            .ToListAsync();
        return View(training);
    }

    public async Task<IActionResult> Calendar()
    {
        var events = await _context.Events
            .Where(e => e.IsActive)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
        return View(events);
    }

    public async Task<IActionResult> Blog()
    {
        var announcements = await _context.Announcements
            .Where(a => a.IsPublished)
            .OrderByDescending(a => a.PublishDate)
            .ToListAsync();
        return View(announcements);
    }

    public async Task<IActionResult> BlogDetail(int id)
    {
        var announcement = await _context.Announcements
            .FirstOrDefaultAsync(a => a.Id == id && a.IsPublished);
        if (announcement == null) return NotFound();

        ViewBag.RecentPosts = await _context.Announcements
            .Where(a => a.IsPublished && a.Id != id)
            .OrderByDescending(a => a.PublishDate)
            .Take(3)
            .ToListAsync();
        return View(announcement);
    }

    public async Task<IActionResult> References()
    {
        var references = await _context.References
            .Where(r => r.IsActive)
            .OrderBy(r => r.SortOrder)
            .ToListAsync();
        return View(references);
    }

    public async Task<IActionResult> Advantages()
    {
        var advantages = await _context.PartnerAdvantages
            .Where(a => a.IsActive)
            .OrderBy(a => a.SortOrder)
            .ToListAsync();
        return View(advantages);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(string email, string? fullName)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["NewsletterError"] = "Lütfen e-posta adresinizi giriniz.";
            return Redirect(Request.Headers["Referer"].ToString() + "#newsletter");
        }

        var existing = await _context.Subscribers.FirstOrDefaultAsync(s => s.Email == email);
        if (existing != null)
        {
            if (existing.IsActive)
            {
                TempData["NewsletterError"] = "Bu e-posta adresi zaten kayıtlı.";
            }
            else
            {
                existing.IsActive = true;
                existing.UnsubscribedAt = null;
                existing.SubscribedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                TempData["NewsletterSuccess"] = "Tekrar abone olduğunuz için teşekkürler!";
            }
        }
        else
        {
            _context.Subscribers.Add(new Subscriber
            {
                Email = email,
                FullName = fullName,
                IsActive = true,
                SubscribedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
            TempData["NewsletterSuccess"] = "Bültenimize başarıyla abone oldunuz!";
        }

        return Redirect(Request.Headers["Referer"].ToString() + "#newsletter");
    }

    public async Task<IActionResult> Unsubscribe(string email)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(s => s.Email == email && s.IsActive);
        if (subscriber != null)
        {
            subscriber.IsActive = false;
            subscriber.UnsubscribedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        ViewBag.Email = email;
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(string name, string email, string phone, string company, string subject, string message)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
            {
                TempData["Error"] = "Lütfen zorunlu alanları doldurunuz.";
                return View();
            }

            // Email sending configuration (configure in appsettings.json)
            var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
            var smtpUsername = _configuration["EmailSettings:Username"];
            var smtpPassword = _configuration["EmailSettings:Password"];
            var recipientEmail = _configuration["EmailSettings:RecipientEmail"] ?? "info@kurumsalegitim.com";

            // If SMTP is not configured, just show success message (for demo purposes)
            if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogInformation($"Contact form submitted: Name={name}, Email={email}, Subject={subject}");
                TempData["Success"] = "Mesajınız başarıyla alındı. En kısa sürede size dönüş yapacağız.";
                return View();
            }

            // Build email
            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername, "Kurumsal Eğitim Web Sitesi"),
                Subject = $"[İletişim Formu] {subject}",
                IsBodyHtml = true,
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <h2 style='color: #0d47a1;'>Yeni İletişim Formu Mesajı</h2>
                        <table style='border-collapse: collapse; width: 100%; max-width: 600px;'>
                            <tr style='background-color: #f5f5f5;'>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;'>Ad Soyad:</td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{WebUtility.HtmlEncode(name)}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;'>E-posta:</td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{WebUtility.HtmlEncode(email)}</td>
                            </tr>
                            <tr style='background-color: #f5f5f5;'>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;'>Telefon:</td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{WebUtility.HtmlEncode(phone ?? "-")}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;'>Kurum:</td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{WebUtility.HtmlEncode(company ?? "-")}</td>
                            </tr>
                            <tr style='background-color: #f5f5f5;'>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;'>Konu:</td>
                                <td style='padding: 10px; border: 1px solid #ddd;'>{WebUtility.HtmlEncode(subject)}</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd; font-weight: bold;' colspan='2'>Mesaj:</td>
                            </tr>
                            <tr>
                                <td style='padding: 10px; border: 1px solid #ddd;' colspan='2'>{WebUtility.HtmlEncode(message).Replace("\n", "<br>")}</td>
                            </tr>
                        </table>
                        <p style='color: #666; font-size: 12px; margin-top: 20px;'>
                            Bu mesaj Kurumsal Eğitim web sitesi iletişim formundan gönderilmiştir.
                        </p>
                    </body>
                    </html>"
            };
            mailMessage.To.Add(recipientEmail);
            mailMessage.ReplyToList.Add(new MailAddress(email, name));

            // Send email
            using var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);

            TempData["Success"] = "Mesajınız başarıyla gönderildi. En kısa sürede size dönüş yapacağız.";
            _logger.LogInformation($"Contact email sent successfully: {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending contact email");
            TempData["Error"] = "Mesaj gönderilirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
