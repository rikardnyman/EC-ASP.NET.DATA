using Data.Entities;

namespace Data.Dtos
{
    public static class ProjectMapper
    {
        public static Project ToDto(this ProjectEntity entity)
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
                StatusId = entity.StatusId
            };
        }

        public static List<Project> ToDtoList(this IEnumerable<ProjectEntity> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
