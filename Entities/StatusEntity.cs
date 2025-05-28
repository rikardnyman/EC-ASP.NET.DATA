using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class StatusEntity
    {
        [Key]
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}