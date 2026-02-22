using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KurumsalEgitimSitesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Author = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ColorCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EventType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: true),
                    IsLimitedCapacity = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LogoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Sector = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IconColorClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Duration = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TargetAudience = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CertificateType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "Id", "Author", "Content", "CreatedAt", "ImageUrl", "IsPublished", "PublishDate", "Summary", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Kurumsal Eğitim", "2025 yılı için hazırladığımız kapsamlı eğitim kataloğumuz yayına alınmıştır. Kamu kurumları ve özel sektör için özel olarak tasarlanmış programlarımızı inceleyebilirsiniz.", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, true, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Yeni dönem eğitim programlarımız açıklandı.", "2025 Yılı Eğitim Kataloğumuz Yayında!", "Duyuru", null },
                    { 2, "Kurumsal Eğitim", "2025 yaz dönemi Antalya seminer programımız açıklanmıştır. Sınırlı kontenjanlarımız hızla dolmaktadır. Detaylı bilgi ve kayıt için bizimle iletişime geçebilirsiniz.", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2025 yaz dönemi Antalya seminerleri başlıyor.", "Antalya Seminer Programı Açıklandı", "Duyuru", null },
                    { 3, "Dr. Ahmet Yılmaz", "Dijital dönüşüm sürecinde kurumların en büyük yatırımı insan kaynağına yapılmalıdır. Bu yazımızda dijital dönüşümde kurumsal eğitimin rolünü ve önemini ele alıyoruz.", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, true, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Dijital çağda kurumsal eğitim neden kritik?", "Dijital Dönüşümde Kurumsal Eğitimin Önemi", "Blog", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ColorCode", "CreatedAt", "Description", "IconClass", "IsActive", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 1, "#f97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çalışanlarınızın kişisel ve profesyonel gelişimini destekleyen eğitim programları.", "fas fa-user-graduate", true, "Kişisel Gelişim", 1 },
                    { 2, "#2563eb", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dijital dönüşüm süreçlerinizi destekleyen güncel teknoloji eğitimleri.", "fas fa-laptop-code", true, "Teknoloji", 2 },
                    { 3, "#dc2626", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kurumunuzun bilgi güvenliğini sağlamak için kritik siber güvenlik eğitimleri.", "fas fa-shield-alt", true, "Siber Güvenlik", 3 },
                    { 4, "#059669", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İş verimliliğinizi artıracak Microsoft Office uygulamaları eğitimleri.", "fab fa-microsoft", true, "MS Office", 4 },
                    { 5, "#7c3aed", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resmi protokol kuralları ve kurumsal temsil becerilerini geliştiren eğitimler.", "fas fa-handshake", true, "Protokol ve Teşrifat", 5 },
                    { 6, "#0891b2", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kamu personeline özel tasarlanmış hizmet içi eğitim programları.", "fas fa-landmark", true, "Kamu Kurumları", 6 },
                    { 7, "#ea580c", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kişisel Verilerin Korunması Kanunu kapsamında zorunlu farkındalık ve uyum eğitimleri.", "fas fa-user-lock", true, "KVKK", 7 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Description", "EndDate", "EventType", "ImageUrl", "IsActive", "IsLimitedCapacity", "Location", "StartDate", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 50, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kamu ihale mevzuatındaki güncel değişiklikler ve uygulamalar.", new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Seminer", null, true, true, "Kundu Deluxe Otel, Antalya", new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), "İhale Uygulamaları ve Güncel Mevzuat Eğitimi", null },
                    { 2, 40, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Belediye mevzuatı ve denetim süreçleri hakkında kapsamlı eğitim.", new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seminer", null, true, true, "Luxury Resort & Spa, Antalya", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Belediyeler için Mevzuat ve Denetim Eğitimi", null },
                    { 3, 45, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İhale süreçleri ve mevzuat uygulamaları.", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Seminer", null, true, true, "Paradise Kundu Otel, Antalya", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Kamu İhale Mevzuatı ve Uygulamaları Eğitimi", null }
                });

            migrationBuilder.InsertData(
                table: "References",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Description", "IconClass", "IconColorClass", "IsActive", "LogoUrl", "Sector", "SortOrder" },
                values: new object[,]
                {
                    { 1, "T.C. Gümrük Müsteşarlığı", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Liderlik ve İletişim Eğitimleri", "fas fa-landmark", "red", true, null, "Kamu", 1 },
                    { 2, "İstanbul Büyükşehir Belediyesi", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mevzuat ve Denetim Eğitimleri", "fas fa-university", "blue", true, null, "Kamu", 2 },
                    { 3, "Arçelik", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İhale Danışmanlığı ve Eğitim Programları", "fas fa-industry", "orange", true, null, "Özel Sektör", 3 },
                    { 4, "Naile Danışmanlık", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kurumsal Danışmanlık ve Eğitim", "fas fa-handshake", "red", true, null, "Özel Sektör", 4 }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "CategoryId", "CertificateType", "CreatedAt", "Description", "Duration", "ImageUrl", "IsActive", "IsFeatured", "Level", "Location", "ShortDescription", "SortOrder", "TargetAudience", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Etkili liderlik becerileri, ekip yönetimi, motivasyon teknikleri ve stratejik düşünme konularını kapsayan kapsamlı eğitim programı.", "2 Gün (16 Saat)", null, true, true, "İleri", null, "Etkili liderlik becerileri ve ekip yönetimi.", 1, "Orta ve Üst Düzey Yöneticiler", "Liderlik ve Yöneticilik", null },
                    { 2, 1, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sözlü ve yazılı iletişim, aktif dinleme, empati, beden dili ve profesyonel iletişim teknikleri eğitimi.", "1 Gün (8 Saat)", null, true, true, "Temel", null, "Sözlü ve yazılı iletişim teknikleri.", 2, "Tüm Çalışanlar", "Etkili İletişim Becerileri", null },
                    { 3, 1, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Önceliklendirme, planlama teknikleri, stres kaynaklarını tanıma ve başa çıkma stratejileri.", "1 Gün (8 Saat)", null, true, false, "Temel", null, "Önceliklendirme ve stresle başa çıkma.", 3, "Tüm Çalışanlar", "Zaman ve Stres Yönetimi", null },
                    { 4, 2, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dijital çağda iş süreçleri, teknoloji trendleri ve kurumlarda dijital dönüşüm stratejileri.", "1 Gün (8 Saat)", null, true, true, "Temel", null, "Dijital çağda iş süreçleri ve teknoloji trendleri.", 1, "Tüm Çalışanlar", "Dijital Dönüşüm Farkındalık", null },
                    { 5, 2, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Veri toplama, analiz yöntemleri, raporlama teknikleri ve veri odaklı karar verme süreçleri.", "2 Gün (16 Saat)", null, true, false, "Orta", null, "Veri toplama, analiz ve raporlama.", 2, "Veri ile Çalışan Personel", "Veri Analitiği Temelleri", null },
                    { 6, 3, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sosyal mühendislik saldırıları, phishing, güvenli parola kullanımı ve temel güvenlik önlemleri.", "1 Gün (8 Saat)", null, true, true, "Temel", null, "Temel güvenlik önlemleri ve farkındalık.", 1, "Tüm Çalışanlar", "Siber Güvenlik Farkındalık", null },
                    { 7, 3, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ISO 27001, güvenlik politikaları, risk yönetimi ve olay müdahale süreçleri.", "2 Gün (16 Saat)", null, true, false, "İleri", null, "ISO 27001 ve güvenlik politikaları.", 2, "IT ve Güvenlik Personeli", "Kurumsal Bilgi Güvenliği", null },
                    { 8, 4, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pivot tablolar, makrolar, ileri formüller, veri analizi ve dashboard oluşturma teknikleri.", "2 Gün (16 Saat)", null, true, false, "İleri", null, "Pivot tablolar, makrolar ve ileri formüller.", 1, "Veri ile Çalışan Personel", "Excel İleri Seviye", null },
                    { 9, 4, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Profesyonel sunum tasarımı, animasyon teknikleri ve etkili sunum becerileri.", "1 Gün (8 Saat)", null, true, false, "Orta", null, "Profesyonel sunum tasarımı.", 2, "Sunum Yapan Personel", "PowerPoint Profesyonel", null },
                    { 10, 5, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Devlet protokolü, masa düzeni, tören yönetimi, diplomatik nezaket kuralları.", "2 Gün (16 Saat)", null, true, false, "Orta", null, "Devlet protokolü ve tören yönetimi.", 1, "Kamu Personeli, Protokol Görevlileri", "Resmi Protokol Kuralları", null },
                    { 11, 5, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Profesyonel görünüm, davranış standartları, kurumsal imaj yönetimi.", "1 Gün (8 Saat)", null, true, false, "Temel", null, "Profesyonel görünüm ve davranış standartları.", 2, "Tüm Çalışanlar", "Kurumsal Temsil ve Davranış", null },
                    { 12, 6, "Hizmet İçi Eğitim Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kamu kurumlarının ihtiyaçlarına özel tasarlanmış kapsamlı hizmet içi eğitimler.", "Kuruma Özel", null, true, false, "Karma", null, "Kamu kurumlarına özel kapsamlı eğitimler.", 1, "Kamu Personeli", "Hizmet İçi Eğitim Programı", null },
                    { 13, 6, "Katılım Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Güncel mevzuat değişiklikleri, yönetmelik uygulamaları ve yasal düzenlemeler.", "1-2 Gün", null, true, false, "Orta", null, "Güncel mevzuat değişiklikleri ve uygulamaları.", 2, "Kamu Personeli", "Mevzuat ve Yönetmelik Eğitimi", null },
                    { 14, 7, "KVKK Eğitim Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "KVKK kapsamı, temel kavramlar, veri sorumlusu yükümlülükleri ve çalışan sorumlulukları.", "1 Gün (8 Saat)", null, true, false, "Temel", null, "KVKK temel kavramlar ve çalışan sorumlulukları.", 1, "Tüm Çalışanlar", "KVKK Farkındalık Eğitimi", null },
                    { 15, 7, "KVKK Uzman Sertifikası", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "VERBİS kaydı, politika hazırlama, veri envanteri, teknik ve idari tedbirler.", "2 Gün (16 Saat)", null, true, false, "İleri", null, "VERBİS kaydı ve politika hazırlama.", 2, "KVKK Sorumluları, IT Personeli", "KVKK Uyum Süreci", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CategoryId",
                table: "Trainings",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
