using Business.Models;
using Data.Models;

namespace WebApp.ViewModels;

public class ProjectsViewModel
{
    public AddProjectForm AddProjectForm { get; set; } = new AddProjectForm();
    public IEnumerable<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
