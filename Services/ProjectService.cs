using Data.Entities;
using Data.Repositories;

namespace Data.Services
{
    public interface IProjectService
    {
        Task<ProjectEntity> CreateProject(ProjectEntity project);
        Task<ProjectEntity?> UpdateProject(int id, ProjectEntity project);
        Task<bool> DeleteProject(int id);
        Task<ProjectEntity?> GetProjectById(int id);
        Task<List<ProjectEntity>> GetAllProjects();
    }

    public class ProjectService : IProjectService
    {

        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectEntity> CreateProject(ProjectEntity project)
        {
            return await _projectRepository.CreateProject(project);
        }

        public async Task<ProjectEntity?> UpdateProject(int id, ProjectEntity updatedProject)
        {
            return await _projectRepository.UpdateProject(id, updatedProject);
        }

        public async Task<bool> DeleteProject(int id)
        {
            return await _projectRepository.DeleteProject(id);
        }

        public async Task<ProjectEntity?> GetProjectById(int id)
        {
            return await _projectRepository.GetProjectById(id);
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }
    }
}
