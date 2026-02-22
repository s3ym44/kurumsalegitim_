using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(150)]
    [Display(Name = "Kategori Adı")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [StringLength(100)]
    [Display(Name = "İkon (CSS Sınıfı)")]
    public string? IconClass { get; set; }

    [StringLength(100)]
    [Display(Name = "Renk Kodu")]
    public string? ColorCode { get; set; }

    [Display(Name = "Sıralama")]
    public int SortOrder { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Training> Trainings { get; set; } = new List<Training>();
}
