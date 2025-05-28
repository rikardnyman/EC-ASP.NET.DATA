using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ProjectEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ImagePath { get; set; }
        public string ProjectName { get; set; } = null!;

        public string? Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public string? Budget { get; set; }

        public StatusEntity? Status { get; set; }
        public int StatusId { get; set; }


        [ForeignKey("ClientId")]
        public ClientEntity? Client { get; set; }

        public int ClientId { get; set; }





        public ProjectEntity ToEntity()
        {
            return new ProjectEntity
            {
                Id = this.Id,
                ImagePath = this.ImagePath,
                ProjectName = this.ProjectName,
                Description = this.Description,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Budget = this.Budget,
                ClientId = this.ClientId,
                StatusId = this.StatusId

            };
        }

    }
}
