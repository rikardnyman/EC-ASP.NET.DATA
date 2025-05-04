using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    public interface IProjectRepository
    {
        Task<List<ProjectEntity>> GetAllProjects();
        Task<ProjectEntity?> GetProjectById(int id);
        Task<ProjectEntity> CreateProject(ProjectEntity project);
        Task<ProjectEntity?> UpdateProject(int id, ProjectEntity project);
        Task<bool> DeleteProject(int id);
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly Data.Contexts.AppDbContext _context;

        public ProjectRepository(Data.Contexts.AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            return await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Status)
                .Include(p => p.User)
                .ToListAsync();
        }


        public async Task<ProjectEntity?> GetProjectById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<ProjectEntity> CreateProject(ProjectEntity project)
        {
            ArgumentNullException.ThrowIfNull(project);

            var client = await GetClientOrThrowAsync(project.ClientId);
            project.Client = client;

            var status = await _context.Statuses.FindAsync(project.StatusId);
            project.Status = status ?? throw new ArgumentNullException("Status not found");

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<ProjectEntity?> UpdateProject(int id, ProjectEntity updatedProject)
        {
            ArgumentNullException.ThrowIfNull(updatedProject);

            var existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null) return null;

            var client = await GetClientOrThrowAsync(updatedProject.ClientId);

            existingProject.Client = client;
            existingProject.ProjectName = updatedProject.ProjectName;
            existingProject.Description = updatedProject.Description;
            existingProject.StatusId = updatedProject.StatusId;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.EndDate = updatedProject.EndDate;
            existingProject.Budget = updatedProject.Budget;

            await _context.SaveChangesAsync();

            return existingProject;
        }

        public async Task<bool> DeleteProject(int id)
        {
            if (id <= 0) return false;

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<ClientEntity> GetClientOrThrowAsync(string clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            return client ?? throw new ArgumentNullException("Client not found");
        }
    }

}

