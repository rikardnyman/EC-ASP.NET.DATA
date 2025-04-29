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
            return await _context.Projects.Include(p => p.Client).ToListAsync();
        }

        public async Task<ProjectEntity?> GetProjectById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<ProjectEntity> CreateProject(ProjectEntity newProject)
        {
            if (newProject == null)
            {
                throw new ArgumentNullException(nameof(newProject));
            }

            ClientEntity? client = await _context.Clients.FindAsync(newProject.ClientId);
            if (client == null)
            {
                throw new ArgumentNullException("Client not found");
            }

            newProject.Client = client;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public async Task<ProjectEntity?> UpdateProject(int id, ProjectEntity updatedProject)
        {
            if (updatedProject == null) throw new ArgumentNullException(nameof(updatedProject));

            ProjectEntity? existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null) return null;

            ClientEntity? client = await _context.Clients.FindAsync(updatedProject.ClientId);
            if (client == null)
            {
                throw new ArgumentNullException("Customer not found");
            }

            existingProject.Client = client;
            existingProject.ProjectName = updatedProject.ProjectName;
            existingProject.Description = updatedProject.Description;
            existingProject.Status = updatedProject.Status;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.EndDate = updatedProject.EndDate;
            existingProject.Budget = updatedProject.Budget;

            await _context.SaveChangesAsync();

            return existingProject;
        }

        public async Task<bool> DeleteProject(int id)
        {
            ProjectEntity? project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

