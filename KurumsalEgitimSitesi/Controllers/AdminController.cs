using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KurumsalEgitimSitesi.Data;
using KurumsalEgitimSitesi.Models;
using System.Net;
using System.Net.Mail;

namespace KurumsalEgitimSitesi.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext context, IConfiguration configuration, ILogger<AdminController> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    // ==================== DASHBOARD ====================
    public async Task<IActionResult> Index()
    {
        ViewBag.CategoryCount = await _context.Categories.CountAsync();
        ViewBag.TrainingCount = await _context.Trainings.CountAsync();
        ViewBag.EventCount = await _context.Events.CountAsync();
        ViewBag.AnnouncementCount = await _context.Announcements.CountAsync();
        ViewBag.ReferenceCount = await _context.References.CountAsync();
        ViewBag.UpcomingEvents = await _context.Events
            .Where(e => e.StartDate >= DateTime.UtcNow && e.IsActive)
            .OrderBy(e => e.StartDate)
            .Take(5)
            .ToListAsync();
        ViewBag.RecentAnnouncements = await _context.Announcements
            .Where(a => a.IsPublished)
            .OrderByDescending(a => a.PublishDate)
            .Take(5)
            .ToListAsync();
        return View();
    }

    // ==================== CATEGORIES ====================
    public async Task<IActionResult> Categories()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
        return View(categories);
    }

    public IActionResult CategoryCreate()
    {
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CategoryCreate(Category category)
    {
        if (ModelState.IsValid)
        {
            category.CreatedAt = DateTime.UtcNow;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Kategori başarıyla eklendi.";
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    public async Task<IActionResult> CategoryEdit(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CategoryEdit(int id, Category category)
    {
        if (id != category.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CategoryDelete(int id)
    {
        var category = await _context.Categories.Include(c => c.Trainings).FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return NotFound();
        if (category.Trainings.Any())
        {
            TempData["Error"] = "Bu kategoriye bağlı eğitimler var. Önce eğitimleri silin veya taşıyın.";
            return RedirectToAction(nameof(Categories));
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Kategori başarıyla silindi.";
        return RedirectToAction(nameof(Categories));
    }

    // ==================== TRAININGS ====================
    public async Task<IActionResult> Trainings()
    {
        var trainings = await _context.Trainings
            .Include(t => t.Category)
            .OrderBy(t => t.CategoryId).ThenBy(t => t.SortOrder)
            .ToListAsync();
        return View(trainings);
    }

    public async Task<IActionResult> TrainingCreate()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name");
        return View(new Training());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainingCreate(Training training)
    {
        if (ModelState.IsValid)
        {
            training.CreatedAt = DateTime.UtcNow;
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Eğitim başarıyla eklendi.";
            return RedirectToAction(nameof(Trainings));
        }
        ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name", training.CategoryId);
        return View(training);
    }

    public async Task<IActionResult> TrainingEdit(int id)
    {
        var training = await _context.Trainings.FindAsync(id);
        if (training == null) return NotFound();
        ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name", training.CategoryId);
        return View(training);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainingEdit(int id, Training training)
    {
        if (id != training.Id) return NotFound();
        if (ModelState.IsValid)
        {
            training.UpdatedAt = DateTime.UtcNow;
            _context.Update(training);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Eğitim başarıyla güncellendi.";
            return RedirectToAction(nameof(Trainings));
        }
        ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.SortOrder).ToListAsync(), "Id", "Name", training.CategoryId);
        return View(training);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainingDelete(int id)
    {
        var training = await _context.Trainings.FindAsync(id);
        if (training == null) return NotFound();
        _context.Trainings.Remove(training);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Eğitim başarıyla silindi.";
        return RedirectToAction(nameof(Trainings));
    }

    // ==================== EVENTS ====================
    public async Task<IActionResult> Events()
    {
        var events = await _context.Events
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
        return View(events);
    }

    public IActionResult EventCreate()
    {
        return View(new Event());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EventCreate(Event eventItem)
    {
        if (ModelState.IsValid)
        {
            eventItem.CreatedAt = DateTime.UtcNow;
            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Etkinlik başarıyla eklendi.";
            return RedirectToAction(nameof(Events));
        }
        return View(eventItem);
    }

    public async Task<IActionResult> EventEdit(int id)
    {
        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null) return NotFound();
        return View(eventItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EventEdit(int id, Event eventItem)
    {
        if (id != eventItem.Id) return NotFound();
        if (ModelState.IsValid)
        {
            eventItem.UpdatedAt = DateTime.UtcNow;
            _context.Update(eventItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Etkinlik başarıyla güncellendi.";
            return RedirectToAction(nameof(Events));
        }
        return View(eventItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EventDelete(int id)
    {
        var eventItem = await _context.Events.FindAsync(id);
        if (eventItem == null) return NotFound();
        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Etkinlik başarıyla silindi.";
        return RedirectToAction(nameof(Events));
    }

    // ==================== ANNOUNCEMENTS ====================
    public async Task<IActionResult> Announcements()
    {
        var announcements = await _context.Announcements
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
        return View(announcements);
    }

    public IActionResult AnnouncementCreate()
    {
        return View(new Announcement());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AnnouncementCreate(Announcement announcement)
    {
        if (ModelState.IsValid)
        {
            announcement.CreatedAt = DateTime.UtcNow;
            if (announcement.IsPublished && announcement.PublishDate == null)
                announcement.PublishDate = DateTime.UtcNow;
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Duyuru başarıyla eklendi.";
            return RedirectToAction(nameof(Announcements));
        }
        return View(announcement);
    }

    public async Task<IActionResult> AnnouncementEdit(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null) return NotFound();
        return View(announcement);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AnnouncementEdit(int id, Announcement announcement)
    {
        if (id != announcement.Id) return NotFound();
        if (ModelState.IsValid)
        {
            announcement.UpdatedAt = DateTime.UtcNow;
            if (announcement.IsPublished && announcement.PublishDate == null)
                announcement.PublishDate = DateTime.UtcNow;
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Duyuru başarıyla güncellendi.";
            return RedirectToAction(nameof(Announcements));
        }
        return View(announcement);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AnnouncementDelete(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null) return NotFound();
        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Duyuru başarıyla silindi.";
        return RedirectToAction(nameof(Announcements));
    }

    // ==================== REFERENCES ====================
    public async Task<IActionResult> References()
    {
        var references = await _context.References
            .OrderBy(r => r.SortOrder)
            .ToListAsync();
        return View(references);
    }

    public IActionResult ReferenceCreate()
    {
        return View(new Reference());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReferenceCreate(Reference reference)
    {
        if (ModelState.IsValid)
        {
            reference.CreatedAt = DateTime.UtcNow;
            _context.References.Add(reference);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Referans başarıyla eklendi.";
            return RedirectToAction(nameof(References));
        }
        return View(reference);
    }

    public async Task<IActionResult> ReferenceEdit(int id)
    {
        var reference = await _context.References.FindAsync(id);
        if (reference == null) return NotFound();
        return View(reference);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReferenceEdit(int id, Reference reference)
    {
        if (id != reference.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(reference);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Referans başarıyla güncellendi.";
            return RedirectToAction(nameof(References));
        }
        return View(reference);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReferenceDelete(int id)
    {
        var reference = await _context.References.FindAsync(id);
        if (reference == null) return NotFound();
        _context.References.Remove(reference);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Referans başarıyla silindi.";
        return RedirectToAction(nameof(References));
    }

    // ==================== PARTNER ADVANTAGES ====================
    public async Task<IActionResult> Advantages()
    {
        var advantages = await _context.PartnerAdvantages.OrderBy(a => a.SortOrder).ToListAsync();
        return View(advantages);
    }

    public IActionResult AdvantageCreate()
    {
        return View(new PartnerAdvantage());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdvantageCreate(PartnerAdvantage advantage)
    {
        if (ModelState.IsValid)
        {
            advantage.CreatedAt = DateTime.UtcNow;
            _context.PartnerAdvantages.Add(advantage);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Avantaj başarıyla eklendi.";
            return RedirectToAction(nameof(Advantages));
        }
        return View(advantage);
    }

    public async Task<IActionResult> AdvantageEdit(int id)
    {
        var advantage = await _context.PartnerAdvantages.FindAsync(id);
        if (advantage == null) return NotFound();
        return View(advantage);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdvantageEdit(int id, PartnerAdvantage advantage)
    {
        if (id != advantage.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(advantage);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Avantaj başarıyla güncellendi.";
            return RedirectToAction(nameof(Advantages));
        }
        return View(advantage);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdvantageDelete(int id)
    {
        var advantage = await _context.PartnerAdvantages.FindAsync(id);
        if (advantage == null) return NotFound();
        _context.PartnerAdvantages.Remove(advantage);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Avantaj başarıyla silindi.";
        return RedirectToAction(nameof(Advantages));
    }

    // ==================== SUBSCRIBERS ====================
    public async Task<IActionResult> Subscribers()
    {
        var subscribers = await _context.Subscribers.OrderByDescending(s => s.SubscribedAt).ToListAsync();
        return View(subscribers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubscriberToggle(int id)
    {
        var subscriber = await _context.Subscribers.FindAsync(id);
        if (subscriber == null) return NotFound();
        subscriber.IsActive = !subscriber.IsActive;
        if (!subscriber.IsActive) subscriber.UnsubscribedAt = DateTime.UtcNow;
        else subscriber.UnsubscribedAt = null;
        await _context.SaveChangesAsync();
        TempData["Success"] = subscriber.IsActive ? "Abone aktifleştirildi." : "Abone pasifleştirildi.";
        return RedirectToAction(nameof(Subscribers));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubscriberDelete(int id)
    {
        var subscriber = await _context.Subscribers.FindAsync(id);
        if (subscriber == null) return NotFound();
        _context.Subscribers.Remove(subscriber);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Abone başarıyla silindi.";
        return RedirectToAction(nameof(Subscribers));
    }

    // ==================== MAIL TEMPLATES ====================
    public async Task<IActionResult> MailTemplates()
    {
        var templates = await _context.MailTemplates.OrderByDescending(m => m.CreatedAt).ToListAsync();
        return View(templates);
    }

    public IActionResult MailTemplateCreate()
    {
        return View(new MailTemplate());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MailTemplateCreate(MailTemplate template)
    {
        if (ModelState.IsValid)
        {
            template.CreatedAt = DateTime.UtcNow;
            _context.MailTemplates.Add(template);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Mail şablonu başarıyla eklendi.";
            return RedirectToAction(nameof(MailTemplates));
        }
        return View(template);
    }

    public async Task<IActionResult> MailTemplateEdit(int id)
    {
        var template = await _context.MailTemplates.FindAsync(id);
        if (template == null) return NotFound();
        return View(template);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MailTemplateEdit(int id, MailTemplate template)
    {
        if (id != template.Id) return NotFound();
        if (ModelState.IsValid)
        {
            template.UpdatedAt = DateTime.UtcNow;
            _context.Update(template);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Mail şablonu başarıyla güncellendi.";
            return RedirectToAction(nameof(MailTemplates));
        }
        return View(template);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MailTemplateDelete(int id)
    {
        var template = await _context.MailTemplates.FindAsync(id);
        if (template == null) return NotFound();
        _context.MailTemplates.Remove(template);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Mail şablonu başarıyla silindi.";
        return RedirectToAction(nameof(MailTemplates));
    }

    // ==================== BULK MAIL ====================
    public async Task<IActionResult> BulkMail()
    {
        ViewBag.Templates = await _context.MailTemplates.Where(m => m.IsActive).OrderBy(m => m.Name).ToListAsync();
        ViewBag.SubscriberCount = await _context.Subscribers.CountAsync(s => s.IsActive);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendBulkMail(int templateId, string? customSubject, string? customBody)
    {
        var template = await _context.MailTemplates.FindAsync(templateId);
        var subscribers = await _context.Subscribers.Where(s => s.IsActive).ToListAsync();

        if (subscribers.Count == 0)
        {
            TempData["Error"] = "Aktif abone bulunmuyor.";
            return RedirectToAction(nameof(BulkMail));
        }

        var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
        var smtpUsername = _configuration["EmailSettings:Username"];
        var smtpPassword = _configuration["EmailSettings:Password"];

        var subject = !string.IsNullOrEmpty(customSubject) ? customSubject : template?.Subject ?? "Kurumsal Eğitim Bülteni";
        var body = !string.IsNullOrEmpty(customBody) ? customBody : template?.HtmlBody ?? "";

        if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
        {
            _logger.LogInformation($"Bulk mail simulated: {subscribers.Count} subscribers, Subject: {subject}");
            TempData["Success"] = $"Toplu mail {subscribers.Count} aboneye gönderilmek üzere kuyruğa alındı. (SMTP yapılandırılmadığı için simüle edildi)";
            return RedirectToAction(nameof(BulkMail));
        }

        int sent = 0;
        int failed = 0;
        using var smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true
        };

        foreach (var subscriber in subscribers)
        {
            try
            {
                var personalizedBody = body.Replace("{{email}}", subscriber.Email)
                                           .Replace("{{name}}", subscriber.FullName ?? "Değerli Abonemiz");
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "Kurumsal Eğitim"),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = personalizedBody
                };
                mailMessage.To.Add(subscriber.Email);
                await smtpClient.SendMailAsync(mailMessage);
                sent++;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {subscriber.Email}");
                failed++;
            }
        }

        TempData["Success"] = $"Toplu mail gönderimi tamamlandı. Başarılı: {sent}, Başarısız: {failed}";
        return RedirectToAction(nameof(BulkMail));
    }
}
