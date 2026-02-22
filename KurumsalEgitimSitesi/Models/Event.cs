using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Etkinlik başlığı zorunludur.")]
    [StringLength(250)]
    [Display(Name = "Etkinlik Başlığı")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
    [Display(Name = "Başlangıç Tarihi")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "Bitiş Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [StringLength(200)]
    [Display(Name = "Lokasyon")]
    public string? Location { get; set; }

    [StringLength(500)]
    [Display(Name = "Görsel URL")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Etkinlik türü zorunludur.")]
    [StringLength(50)]
    [Display(Name = "Etkinlik Türü")]
    public string EventType { get; set; } = "Seminer"; // Seminer, Eğitim, Konferans, Workshop

    [Display(Name = "Kontenjan")]
    public int? Capacity { get; set; }

    [Display(Name = "Sınırlı Kontenjan mı?")]
    public bool IsLimitedCapacity { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Güncellenme Tarihi")]
    public DateTime? UpdatedAt { get; set; }
}
