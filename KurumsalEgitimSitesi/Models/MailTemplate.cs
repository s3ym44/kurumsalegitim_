using System.ComponentModel.DataAnnotations;

namespace KurumsalEgitimSitesi.Models;

public class MailTemplate
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Şablon adı zorunludur.")]
    [StringLength(200)]
    [Display(Name = "Şablon Adı")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Konu zorunludur.")]
    [StringLength(300)]
    [Display(Name = "E-posta Konusu")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "İçerik zorunludur.")]
    [Display(Name = "HTML İçerik")]
    public string HtmlBody { get; set; } = string.Empty;

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Güncellenme Tarihi")]
    public DateTime? UpdatedAt { get; set; }
}
