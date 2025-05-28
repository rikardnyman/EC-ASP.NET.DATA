using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ClientEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
