using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Data.Dtos
{

    public class Project
    {
        public string? Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
        public string? Budget { get; set; }
        public int StatusId { get; set; } = 1;
        public string ClientId { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public ProjectEntity ToEntity()
        {
            return new ProjectEntity
            {
                Id = string.IsNullOrEmpty(Id) ? Guid.NewGuid().ToString() : Id,
                Image = ImagePath,
                ProjectName = ProjectName,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate,
                Budget = decimal.TryParse(Budget, out var budgetValue) ? budgetValue : (decimal?)null,
                StatusId = StatusId,
                ClientId = ClientId,
                UserId = string.IsNullOrEmpty(UserId) ? "test-user" : UserId
            };
        }

        public static Project FromEntity(ProjectEntity entity)
        {
            return new Project
            {
                Id = entity.Id,
                ImagePath = entity.Image,
                ProjectName = entity.ProjectName,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate ?? DateTime.Now,
                Budget = entity.Budget?.ToString(),
                StatusId = entity.StatusId,
                ClientId = entity.ClientId,
                UserId = entity.UserId
            };
        }

        private string SaveImage(IFormFile imageFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine("wwwroot", "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return "/uploads/" + fileName;
        }
    }
}
