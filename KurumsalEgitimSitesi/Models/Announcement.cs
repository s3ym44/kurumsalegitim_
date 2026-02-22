using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class Announcement
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur.")]
    [StringLength(250)]
    [Display(Name = "Başlık")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Kısa Özet")]
    [StringLength(500)]
    public string? Summary { get; set; }

    [Required(ErrorMessage = "İçerik zorunludur.")]
    [Display(Name = "İçerik")]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Görsel URL")]
    public string? ImageUrl { get; set; }

    [StringLength(100)]
    [Display(Name = "Yazar")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "Tür zorunludur.")]
    [StringLength(50)]
    [Display(Name = "Tür")]
    public string Type { get; set; } = "Duyuru"; // Duyuru, Blog, Haber

    [Display(Name = "Yayınlandı mı?")]
    public bool IsPublished { get; set; }

    [Display(Name = "Yayın Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? PublishDate { get; set; }

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Güncellenme Tarihi")]
    public DateTime? UpdatedAt { get; set; }
}
