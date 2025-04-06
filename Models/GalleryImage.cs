using System.ComponentModel.DataAnnotations;

public class GalleryImage
{
    public int Id { get; set; }

    [Required]
    public string ImagePath { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.Now;
}
