using Data.Entities;
using Data.Repositories;


namespace Data.Services
{
    public interface IProjectService
    {
        Task<ProjectEntity> CreateProject(ProjectEntity project);
        Task<ProjectEntity?> UpdateProject(string id, ProjectEntity project);
        Task<bool> DeleteProject(string id);
        Task<ProjectEntity?> GetProjectById(string id);
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

        public async Task<ProjectEntity?> UpdateProject(string id, ProjectEntity updatedProject)
        {
            return await _projectRepository.UpdateProject(id, updatedProject);
        }

        public async Task<bool> DeleteProject(string id)
        {
            return await _projectRepository.DeleteProject(id);
        }

        public async Task<ProjectEntity?> GetProjectById(string id)
        {
            return await _projectRepository.GetProjectById(id);
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }
    }
}
