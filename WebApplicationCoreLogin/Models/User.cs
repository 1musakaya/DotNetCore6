using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WebApplicationCoreLogin.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(20)]
        public string? Name { get; set; } = null;
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required,StringLength(16)]
        public string Password { get; set; }
        public bool Activate { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
