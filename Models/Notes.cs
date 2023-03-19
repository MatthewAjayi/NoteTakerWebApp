using System.ComponentModel.DataAnnotations;

namespace NoteTakerWebApp.Models
{
    public class Notes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }    
        public string? Description { get; set; } 
        public int? UserId { get; set; }
    }
}
