using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KurumsalEgitimSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddFaz2Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Subject = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    HtmlBody = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerAdvantages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartnerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DiscountDetail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Conditions = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LinkUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerAdvantages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UnsubscribedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MailTemplates",
                columns: new[] { "Id", "CreatedAt", "HtmlBody", "IsActive", "Name", "Subject", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#2563eb;'>Hoş Geldiniz!</h2><p>Bültenimize abone olduğunuz için teşekkür ederiz. Artık en güncel eğitim fırsatları ve duyurulardan haberdar olacaksınız.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", true, "Hoş Geldiniz", "Bültenimize Hoş Geldiniz!", null },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#2563eb;'>Yeni Eğitim Programı</h2><p>Yeni eğitim programımız hakkında detaylı bilgi almak için web sitemizi ziyaret edin.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", true, "Yeni Eğitim Duyurusu", "Yeni Eğitim Programımız Açıklandı!", null },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "<html><body style='font-family:Arial,sans-serif;'><div style='max-width:600px;margin:0 auto;padding:20px;'><h2 style='color:#f97316;'>Seminer Daveti</h2><p>Yaklaşan seminerimize katılmanızı isteriz. Detaylar için web sitemizi ziyaret edin.</p><p>Saygılarımızla,<br><strong>Kurumsal Eğitim</strong></p></div></body></html>", true, "Seminer Daveti", "Seminerimize Davetlisiniz!", null }
                });

            migrationBuilder.InsertData(
                table: "PartnerAdvantages",
                columns: new[] { "Id", "Category", "Conditions", "CreatedAt", "Description", "DiscountDetail", "IconClass", "IsActive", "LinkUrl", "LogoUrl", "PartnerName", "SortOrder", "Title" },
                values: new object[,]
                {
                    { 1, "Üniversite", "Eğitim sertifikası ibraz edilmelidir. 2025 yılı kayıtları için geçerlidir.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Eğitim katılımcılarına Ankara Üniversitesi yüksek lisans programlarında %20 indirim.", "%20 İndirim", "fas fa-university", true, null, null, "Ankara Üniversitesi", 1, "Yüksek Lisans %20 İndirim" },
                    { 2, "Üniversite", "Son 2 yıl içinde alınmış eğitim sertifikası gereklidir.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ITÜ doktora programlarında eğitim katılımcılarına özel %15 indirim fırsatı.", "%15 İndirim", "fas fa-graduation-cap", true, null, null, "İstanbul Teknik Üniversitesi", 2, "Doktora Programı %15 İndirim" },
                    { 3, "Üniversite", "Minimum 3 eğitime katılım şartı aranmaktadır.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "BAU MBA programında eğitim katılımcılarına %25 burs imkanı.", "%25 Burs", "fas fa-award", true, null, null, "Bahçeşehir Üniversitesi", 3, "MBA Programı %25 Burs" },
                    { 4, "Akaryakıt", "Katılımcı kartı ile tüm Opet istasyonlarında geçerlidir.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tüm Opet istasyonlarında eğitim katılımcılarına özel akaryakıt indirimi.", "%5 İndirim", "fas fa-gas-pump", true, null, null, "Opet", 4, "Akaryakıtta %5 İndirim" },
                    { 5, "Akaryakıt", "Shell ClubSmart kart ile birlikte kullanılabilir.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Shell istasyonlarında litre başına 1 TL indirim avantajı.", "1 TL/Litre İndirim", "fas fa-gas-pump", true, null, null, "Shell", 5, "Akaryakıtta Litre Başı 1 TL İndirim" },
                    { 6, "Konaklama", "Rezervasyon sırasında kurumsal kod belirtilmelidir.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Türkiye genelindeki Hilton otellerinde eğitim katılımcılarına özel konaklama indirimi.", "%30 İndirim", "fas fa-hotel", true, null, null, "Hilton Hotels", 6, "Konaklama %30 İndirim" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_Email",
                table: "Subscribers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailTemplates");

            migrationBuilder.DropTable(
                name: "PartnerAdvantages");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
