using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KurumsalEgitimSitesi.Models;

public class Training
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Eğitim başlığı zorunludur.")]
    [StringLength(250)]
    [Display(Name = "Eğitim Başlığı")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Kısa Açıklama")]
    [StringLength(500)]
    public string? ShortDescription { get; set; }

    [Display(Name = "Detaylı Açıklama")]
    public string? Description { get; set; }

    [StringLength(500)]
    [Display(Name = "Görsel URL")]
    public string? ImageUrl { get; set; }

    [StringLength(100)]
    [Display(Name = "Süre")]
    public string? Duration { get; set; }

    [StringLength(200)]
    [Display(Name = "Hedef Kitle")]
    public string? TargetAudience { get; set; }

    [StringLength(100)]
    [Display(Name = "Sertifika Türü")]
    public string? CertificateType { get; set; }

    [StringLength(50)]
    [Display(Name = "Seviye")]
    public string? Level { get; set; }

    [StringLength(100)]
    [Display(Name = "Lokasyon")]
    public string? Location { get; set; }

    [Display(Name = "Popüler mi?")]
    public bool IsFeatured { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Sıralama")]
    public int SortOrder { get; set; }

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Güncellenme Tarihi")]
    public DateTime? UpdatedAt { get; set; }

    // Foreign Key
    [Display(Name = "Kategori")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}
