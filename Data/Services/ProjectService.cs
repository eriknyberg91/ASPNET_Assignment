

using System.Linq.Expressions;
using Business.Models;
using Data.Interfaces;
using Data.Models;

namespace Data.Services;

public class ProjectService (IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    public async Task<bool> CreateAsync(ProjectEntity project)
    {
        if (await _projectRepository.GetAsync(p => p.ProjectName == project.ProjectName) != null)
        {
            return false;
        }
        await _projectRepository.CreateAsync(project);
        return true;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }
    public async Task<ProjectEntity> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        return entity ?? null!;
    }

    public async Task<bool> UpdateProjectAsync(int id, EditProjectForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(form.ProjectName)) project.ProjectName = form.ProjectName;
        if (!string.IsNullOrEmpty(form.ClientName)) project.ClientName = form.ClientName;
        if (!string.IsNullOrEmpty(form.Description)) project.Description = form.Description;

        project.IsCompleted = form.IsCompleted;
        project.StartDate = form.StartDate;
        project.EndDate = form.EndDate;
        project.Budget = form.Budget;

        await _projectRepository.UpdateAsync(x => x.Id == id, project);
        return true;
    }
}
    

