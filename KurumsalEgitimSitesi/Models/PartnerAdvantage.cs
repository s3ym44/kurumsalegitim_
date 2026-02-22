using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class PartnerAdvantage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Partner adı zorunludur.")]
    [StringLength(200)]
    [Display(Name = "Partner / Kurum Adı")]
    public string PartnerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Avantaj başlığı zorunludur.")]
    [StringLength(250)]
    [Display(Name = "Avantaj Başlığı")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [StringLength(500)]
    [Display(Name = "Logo URL")]
    public string? LogoUrl { get; set; }

    [StringLength(100)]
    [Display(Name = "İkon (CSS Sınıfı)")]
    public string? IconClass { get; set; }

    [Required(ErrorMessage = "Kategori zorunludur.")]
    [StringLength(50)]
    [Display(Name = "Kategori")]
    public string Category { get; set; } = "Genel"; // Üniversite, Akaryakıt, Konaklama, Teknoloji, Genel

    [StringLength(100)]
    [Display(Name = "İndirim Oranı / Detayı")]
    public string? DiscountDetail { get; set; }

    [StringLength(500)]
    [Display(Name = "Koşullar")]
    public string? Conditions { get; set; }

    [StringLength(500)]
    [Display(Name = "Bağlantı URL")]
    public string? LinkUrl { get; set; }

    [Display(Name = "Sıralama")]
    public int SortOrder { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
