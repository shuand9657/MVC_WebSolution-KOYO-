using System.ComponentModel.DataAnnotations;

namespace DBModelClass
{
    public class Audience
    {
        [Key]
        [Required]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secrect { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
