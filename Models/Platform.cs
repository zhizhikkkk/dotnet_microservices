using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models{
    public class Platform{
        [Key]
        [Required]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public string  Name { get; set; }

        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost {get;set;}

    }
}