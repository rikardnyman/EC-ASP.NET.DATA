using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectEntity>> GetAllProjects();
        Task<ProjectEntity?> GetProjectById(string id);
        Task<ProjectEntity> CreateProject(ProjectEntity project);
        Task<ProjectEntity?> UpdateProject(string id, ProjectEntity project);
        Task<bool> DeleteProject(string id);
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
                .ToListAsync();
        }


        public async Task<ProjectEntity?> GetProjectById(string id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<ProjectEntity> CreateProject(ProjectEntity newProject)
        {
            if (newProject == null)
                throw new ArgumentNullException(nameof(newProject));


            ClientEntity? client = await _context.Clients.FindAsync(newProject.ClientId);
            if (client == null)
                throw new ArgumentException("Client not found");


            StatusEntity? status = await _context.Statuses.FindAsync(newProject.StatusId);
            if (status == null)
                throw new ArgumentException("Status not found");


            newProject.Client = client;
            newProject.Status = status;


            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }


        public async Task<ProjectEntity?> UpdateProject(string id, ProjectEntity updatedProject)
        {
            if (updatedProject == null) throw new ArgumentNullException(nameof(updatedProject));

            ProjectEntity? existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null) return null;

            ClientEntity? client = await _context.Clients.FindAsync(updatedProject.ClientId);
            if (client == null)
            {
                throw new ArgumentNullException("Client not found");
            }

            existingProject.Client = updatedProject.Client;
            existingProject.ClientId = updatedProject.ClientId;
            existingProject.ImagePath = updatedProject.ImagePath;
            existingProject.ProjectName = updatedProject.ProjectName;
            existingProject.Description = updatedProject.Description;
            existingProject.Status = updatedProject.Status;
            existingProject.StatusId = updatedProject.StatusId;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.EndDate = updatedProject.EndDate;
            existingProject.Budget = updatedProject.Budget;



            await _context.SaveChangesAsync();

            return existingProject;
        }

        public async Task<bool> DeleteProject(string id)
        {
            ProjectEntity? project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
