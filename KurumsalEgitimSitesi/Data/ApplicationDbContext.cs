using Microsoft.EntityFrameworkCore;
using KurumsalEgitimSitesi.Models;

namespace KurumsalEgitimSitesi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Reference> References { get; set; }
    public DbSet<PartnerAdvantage> PartnerAdvantages { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<MailTemplate> MailTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---- Category ----
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // ---- Training ----
        modelBuilder.Entity<Training>(entity =>
        {
            entity.ToTable("Trainings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(250);

            entity.HasOne(t => t.Category)
                  .WithMany(c => c.Trainings)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Event ----
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(250);
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(50);
        });

        // ---- Announcement ----
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.ToTable("Announcements");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
        });

        // ---- Reference ----
        modelBuilder.Entity<Reference>(entity =>
        {
            entity.ToTable("References");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
        });

        // ---- PartnerAdvantage ----
        modelBuilder.Entity<PartnerAdvantage>(entity =>
        {
            entity.ToTable("PartnerAdvantages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PartnerName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
        });

        // ---- Subscriber ----
        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.ToTable("Subscribers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(250);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // ---- MailTemplate ----
        modelBuilder.Entity<MailTemplate>(entity =>
        {
            entity.ToTable("MailTemplates");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(300);
            entity.Property(e => e.HtmlBody).IsRequired();
        });

        // ===== SEED DATA =====
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // --- Categories ---
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Kişisel Gelişim", Description = "Çalışanlarınızın kişisel ve profesyonel gelişimini destekleyen eğitim programları.", IconClass = "fas fa-user-graduate", ColorCode = "#f97316", SortOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 2, Name = "Teknoloji", Description = "Dijital dönüşüm süreçlerinizi destekleyen güncel teknoloji eğitimleri.", IconClass = "fas fa-laptop-code", ColorCode = "#2563eb", SortOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 3, Name = "Siber Güvenlik", Description = "Kurumunuzun bilgi güvenliğini sağlamak için kritik siber güvenlik eğitimleri.", IconClass = "fas fa-shield-alt", ColorCode = "#dc2626", SortOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 4, Name = "MS Office", Description = "İş verimliliğinizi artıracak Microsoft Office uygulamaları eğitimleri.", IconClass = "fab fa-microsoft", ColorCode = "#059669", SortOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 5, Name = "Protokol ve Teşrifat", Description = "Resmi protokol kuralları ve kurumsal temsil becerilerini geliştiren eğitimler.", IconClass = "fas fa-handshake", ColorCode = "#7c3aed", SortOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 6, Name = "Kamu Kurumları", Description = "Kamu personeline özel tasarlanmış hizmet içi eğitim programları.", IconClass = "fas fa-landmark", ColorCode = "#0891b2", SortOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category { Id = 7, Name = "KVKK", Description = "Kişisel Verilerin Korunması Kanunu kapsamında zorunlu farkındalık ve uyum eğitimleri.", IconClass = "fas fa-user-lock", ColorCode = "#ea580c", SortOrder = 7, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- Trainings ---
        modelBuilder.Entity<Training>().HasData(
            // Kişisel Gelişim
            new Training { Id = 1, Title = "Liderlik ve Yöneticilik", ShortDescription = "Etkili liderlik becerileri ve ekip yönetimi.", Description = "Etkili liderlik becerileri, ekip yönetimi, motivasyon teknikleri ve stratejik düşünme konularını kapsayan kapsamlı eğitim programı.", Duration = "2 Gün (16 Saat)", TargetAudience = "Orta ve Üst Düzey Yöneticiler", CertificateType = "Katılım Sertifikası", Level = "İleri", IsFeatured = true, IsActive = true, SortOrder = 1, CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 2, Title = "Etkili İletişim Becerileri", ShortDescription = "Sözlü ve yazılı iletişim teknikleri.", Description = "Sözlü ve yazılı iletişim, aktif dinleme, empati, beden dili ve profesyonel iletişim teknikleri eğitimi.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "Katılım Sertifikası", Level = "Temel", IsFeatured = true, IsActive = true, SortOrder = 2, CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 3, Title = "Zaman ve Stres Yönetimi", ShortDescription = "Önceliklendirme ve stresle başa çıkma.", Description = "Önceliklendirme, planlama teknikleri, stres kaynaklarını tanıma ve başa çıkma stratejileri.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "Katılım Sertifikası", Level = "Temel", IsFeatured = false, IsActive = true, SortOrder = 3, CategoryId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // Teknoloji
            new Training { Id = 4, Title = "Dijital Dönüşüm Farkındalık", ShortDescription = "Dijital çağda iş süreçleri ve teknoloji trendleri.", Description = "Dijital çağda iş süreçleri, teknoloji trendleri ve kurumlarda dijital dönüşüm stratejileri.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "Katılım Sertifikası", Level = "Temel", IsFeatured = true, IsActive = true, SortOrder = 1, CategoryId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 5, Title = "Veri Analitiği Temelleri", ShortDescription = "Veri toplama, analiz ve raporlama.", Description = "Veri toplama, analiz yöntemleri, raporlama teknikleri ve veri odaklı karar verme süreçleri.", Duration = "2 Gün (16 Saat)", TargetAudience = "Veri ile Çalışan Personel", CertificateType = "Katılım Sertifikası", Level = "Orta", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // Siber Güvenlik
            new Training { Id = 6, Title = "Siber Güvenlik Farkındalık", ShortDescription = "Temel güvenlik önlemleri ve farkındalık.", Description = "Sosyal mühendislik saldırıları, phishing, güvenli parola kullanımı ve temel güvenlik önlemleri.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "Katılım Sertifikası", Level = "Temel", IsFeatured = true, IsActive = true, SortOrder = 1, CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 7, Title = "Kurumsal Bilgi Güvenliği", ShortDescription = "ISO 27001 ve güvenlik politikaları.", Description = "ISO 27001, güvenlik politikaları, risk yönetimi ve olay müdahale süreçleri.", Duration = "2 Gün (16 Saat)", TargetAudience = "IT ve Güvenlik Personeli", CertificateType = "Katılım Sertifikası", Level = "İleri", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // MS Office
            new Training { Id = 8, Title = "Excel İleri Seviye", ShortDescription = "Pivot tablolar, makrolar ve ileri formüller.", Description = "Pivot tablolar, makrolar, ileri formüller, veri analizi ve dashboard oluşturma teknikleri.", Duration = "2 Gün (16 Saat)", TargetAudience = "Veri ile Çalışan Personel", CertificateType = "Katılım Sertifikası", Level = "İleri", IsFeatured = false, IsActive = true, SortOrder = 1, CategoryId = 4, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 9, Title = "PowerPoint Profesyonel", ShortDescription = "Profesyonel sunum tasarımı.", Description = "Profesyonel sunum tasarımı, animasyon teknikleri ve etkili sunum becerileri.", Duration = "1 Gün (8 Saat)", TargetAudience = "Sunum Yapan Personel", CertificateType = "Katılım Sertifikası", Level = "Orta", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 4, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // Protokol ve Teşrifat
            new Training { Id = 10, Title = "Resmi Protokol Kuralları", ShortDescription = "Devlet protokolü ve tören yönetimi.", Description = "Devlet protokolü, masa düzeni, tören yönetimi, diplomatik nezaket kuralları.", Duration = "2 Gün (16 Saat)", TargetAudience = "Kamu Personeli, Protokol Görevlileri", CertificateType = "Katılım Sertifikası", Level = "Orta", IsFeatured = false, IsActive = true, SortOrder = 1, CategoryId = 5, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 11, Title = "Kurumsal Temsil ve Davranış", ShortDescription = "Profesyonel görünüm ve davranış standartları.", Description = "Profesyonel görünüm, davranış standartları, kurumsal imaj yönetimi.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "Katılım Sertifikası", Level = "Temel", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 5, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // Kamu Kurumları
            new Training { Id = 12, Title = "Hizmet İçi Eğitim Programı", ShortDescription = "Kamu kurumlarına özel kapsamlı eğitimler.", Description = "Kamu kurumlarının ihtiyaçlarına özel tasarlanmış kapsamlı hizmet içi eğitimler.", Duration = "Kuruma Özel", TargetAudience = "Kamu Personeli", CertificateType = "Hizmet İçi Eğitim Sertifikası", Level = "Karma", IsFeatured = false, IsActive = true, SortOrder = 1, CategoryId = 6, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 13, Title = "Mevzuat ve Yönetmelik Eğitimi", ShortDescription = "Güncel mevzuat değişiklikleri ve uygulamaları.", Description = "Güncel mevzuat değişiklikleri, yönetmelik uygulamaları ve yasal düzenlemeler.", Duration = "1-2 Gün", TargetAudience = "Kamu Personeli", CertificateType = "Katılım Sertifikası", Level = "Orta", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 6, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },

            // KVKK
            new Training { Id = 14, Title = "KVKK Farkındalık Eğitimi", ShortDescription = "KVKK temel kavramlar ve çalışan sorumlulukları.", Description = "KVKK kapsamı, temel kavramlar, veri sorumlusu yükümlülükleri ve çalışan sorumlulukları.", Duration = "1 Gün (8 Saat)", TargetAudience = "Tüm Çalışanlar", CertificateType = "KVKK Eğitim Sertifikası", Level = "Temel", IsFeatured = false, IsActive = true, SortOrder = 1, CategoryId = 7, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Training { Id = 15, Title = "KVKK Uyum Süreci", ShortDescription = "VERBİS kaydı ve politika hazırlama.", Description = "VERBİS kaydı, politika hazırlama, veri envanteri, teknik ve idari tedbirler.", Duration = "2 Gün (16 Saat)", TargetAudience = "KVKK Sorumluları, IT Personeli", CertificateType = "KVKK Uzman Sertifikası", Level = "İleri", IsFeatured = false, IsActive = true, SortOrder = 2, CategoryId = 7, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- Events ---
        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Title = "İhale Uygulamaları ve Güncel Mevzuat Eğitimi", Description = "Kamu ihale mevzuatındaki güncel değişiklikler ve uygulamalar.", StartDate = new DateTime(2025, 7, 25, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2025, 7, 27, 0, 0, 0, DateTimeKind.Utc), Location = "Kundu Deluxe Otel, Antalya", EventType = "Seminer", Capacity = 50, IsLimitedCapacity = true, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Event { Id = 2, Title = "Belediyeler için Mevzuat ve Denetim Eğitimi", Description = "Belediye mevzuatı ve denetim süreçleri hakkında kapsamlı eğitim.", StartDate = new DateTime(2025, 9, 5, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2025, 9, 7, 0, 0, 0, DateTimeKind.Utc), Location = "Luxury Resort & Spa, Antalya", EventType = "Seminer", Capacity = 40, IsLimitedCapacity = true, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Event { Id = 3, Title = "Kamu İhale Mevzuatı ve Uygulamaları Eğitimi", Description = "İhale süreçleri ve mevzuat uygulamaları.", StartDate = new DateTime(2025, 10, 3, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2025, 10, 5, 0, 0, 0, DateTimeKind.Utc), Location = "Paradise Kundu Otel, Antalya", EventType = "Seminer", Capacity = 45, IsLimitedCapacity = true, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- Announcements ---
        modelBuilder.Entity<Announcement>().HasData(
            new Announcement { Id = 1, Title = "2025 Yılı Eğitim Kataloğumuz Yayında!", Summary = "Yeni dönem eğitim programlarımız açıklandı.", Content = "2025 yılı için hazırladığımız kapsamlı eğitim kataloğumuz yayına alınmıştır. Kamu kurumları ve özel sektör için özel olarak tasarlanmış programlarımızı inceleyebilirsiniz.", Author = "Kurumsal Eğitim", Type = "Duyuru", IsPublished = true, PublishDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Announcement { Id = 2, Title = "Antalya Seminer Programı Açıklandı", Summary = "2025 yaz dönemi Antalya seminerleri başlıyor.", Content = "2025 yaz dönemi Antalya seminer programımız açıklanmıştır. Sınırlı kontenjanlarımız hızla dolmaktadır. Detaylı bilgi ve kayıt için bizimle iletişime geçebilirsiniz.", Author = "Kurumsal Eğitim", Type = "Duyuru", IsPublished = true, PublishDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Announcement { Id = 3, Title = "Dijital Dönüşümde Kurumsal Eğitimin Önemi", Summary = "Dijital çağda kurumsal eğitim neden kritik?", Content = "Dijital dönüşüm sürecinde kurumların en büyük yatırımı insan kaynağına yapılmalıdır. Bu yazımızda dijital dönüşümde kurumsal eğitimin rolünü ve önemini ele alıyoruz.", Author = "Dr. Ahmet Yılmaz", Type = "Blog", IsPublished = true, PublishDate = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- References ---
        modelBuilder.Entity<Reference>().HasData(
            new Reference { Id = 1, CompanyName = "T.C. Gümrük Müsteşarlığı", Description = "Liderlik ve İletişim Eğitimleri", Sector = "Kamu", IconClass = "fas fa-landmark", IconColorClass = "red", SortOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Reference { Id = 2, CompanyName = "İstanbul Büyükşehir Belediyesi", Description = "Mevzuat ve Denetim Eğitimleri", Sector = "Kamu", IconClass = "fas fa-university", IconColorClass = "blue", SortOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Reference { Id = 3, CompanyName = "Arçelik", Description = "İhale Danışmanlığı ve Eğitim Programları", Sector = "Özel Sektör", IconClass = "fas fa-industry", IconColorClass = "orange", SortOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Reference { Id = 4, CompanyName = "Naile Danışmanlık", Description = "Kurumsal Danışmanlık ve Eğitim", Sector = "Özel Sektör", IconClass = "fas fa-handshake", IconColorClass = "red", SortOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- Partner Advantages ---
        modelBuilder.Entity<PartnerAdvantage>().HasData(
            new PartnerAdvantage { Id = 1, PartnerName = "Ankara Üniversitesi", Title = "Yüksek Lisans %20 İndirim", Description = "Eğitim katılımcılarına Ankara Üniversitesi yüksek lisans programlarında %20 indirim.", IconClass = "fas fa-university", Category = "Üniversite", DiscountDetail = "%20 İndirim", Conditions = "Eğitim sertifikası ibraz edilmelidir. 2025 yılı kayıtları için geçerlidir.", SortOrder = 1, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PartnerAdvantage { Id = 2, PartnerName = "İstanbul Teknik Üniversitesi", Title = "Doktora Programı %15 İndirim", Description = "ITÜ doktora programlarında eğitim katılımcılarına özel %15 indirim fırsatı.", IconClass = "fas fa-graduation-cap", Category = "Üniversite", DiscountDetail = "%15 İndirim", Conditions = "Son 2 yıl içinde alınmış eğitim sertifikası gereklidir.", SortOrder = 2, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PartnerAdvantage { Id = 3, PartnerName = "Bahçeşehir Üniversitesi", Title = "MBA Programı %25 Burs", Description = "BAU MBA programında eğitim katılımcılarına %25 burs imkanı.", IconClass = "fas fa-award", Category = "Üniversite", DiscountDetail = "%25 Burs", Conditions = "Minimum 3 eğitime katılım şartı aranmaktadır.", SortOrder = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PartnerAdvantage { Id = 4, PartnerName = "Opet", Title = "Akaryakıtta %5 İndirim", Description = "Tüm Opet istasyonlarında eğitim katılımcılarına özel akaryakıt indirimi.", IconClass = "fas fa-gas-pump", Category = "Akaryakıt", DiscountDetail = "%5 İndirim", Conditions = "Katılımcı kartı ile tüm Opet istasyonlarında geçerlidir.", SortOrder = 4, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PartnerAdvantage { Id = 5, PartnerName = "Shell", Title = "Akaryakıtta Litre Başı 1 TL İndirim", Description = "Shell istasyonlarında litre başına 1 TL indirim avantajı.", IconClass = "fas fa-gas-pump", Category = "Akaryakıt", DiscountDetail = "1 TL/Litre İndirim", Conditions = "Shell ClubSmart kart ile birlikte kullanılabilir.", SortOrder = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PartnerAdvantage { Id = 6, PartnerName = "Hilton Hotels", Title = "Konaklama %30 İndirim", Description = "Türkiye genelindeki Hilton otellerinde eğitim katılımcılarına özel konaklama indirimi.", IconClass = "fas fa-hotel", Category = "Konaklama", DiscountDetail = "%30 İndirim", Conditions = "Rezervasyon sırasında kurumsal kod belirtilmelidir.", SortOrder = 6, IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        // --- Mail Templates ---
        modelBuilder.Entity<MailTemplate>().HasData(
            new MailTemplate { Id = 1, Name = "Hoş Geldiniz", Subject = "Bültenimize Hoş Geldiniz!", HtmlBody = "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#2563eb;'>Hoş Geldiniz!</h2><p>Bültenimize abone olduğunuz için teşekkür ederiz. Artık en güncel eğitim fırsatları ve duyurulardan haberdar olacaksınız.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new MailTemplate { Id = 2, Name = "Yeni Eğitim Duyurusu", Subject = "Yeni Eğitim Programımız Açıklandı!", HtmlBody = "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#2563eb;'>Yeni Eğitim Programı</h2><p>Yeni eğitim programımız hakkında detaylı bilgi almak için web sitemizi ziyaret edin.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new MailTemplate { Id = 3, Name = "Seminer Daveti", Subject = "Seminerimize Davetlisiniz!", HtmlBody = "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#f97316;'>Seminer Daveti</h2><p>Yaklaşan seminerimize katılmanızı isteriz. Detaylar için web sitemizi ziyaret edin.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
