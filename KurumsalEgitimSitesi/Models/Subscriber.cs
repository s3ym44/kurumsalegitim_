using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class Subscriber
{
    public int Id { get; set; }

    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [StringLength(250)]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Ad Soyad")]
    public string? FullName { get; set; }

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Abone Tarihi")]
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "İptal Tarihi")]
    public DateTime? UnsubscribedAt { get; set; }
}
