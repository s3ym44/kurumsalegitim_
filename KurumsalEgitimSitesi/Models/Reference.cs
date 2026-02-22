using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class Reference
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kurum adı zorunludur.")]
    [StringLength(200)]
    [Display(Name = "Kurum Adı")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Logo URL")]
    public string? LogoUrl { get; set; }

    [StringLength(500)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [StringLength(50)]
    [Display(Name = "Sektör")]
    public string? Sector { get; set; } // Kamu, Özel Sektör, Belediye

    [StringLength(100)]
    [Display(Name = "İkon (CSS Sınıfı)")]
    public string? IconClass { get; set; }

    [StringLength(50)]
    [Display(Name = "İkon Renk Kodu")]
    public string? IconColorClass { get; set; }

    [Display(Name = "Sıralama")]
    public int SortOrder { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
