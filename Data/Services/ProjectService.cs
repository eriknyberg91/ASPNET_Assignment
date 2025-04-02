

using System.Linq.Expressions;
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
}
    

