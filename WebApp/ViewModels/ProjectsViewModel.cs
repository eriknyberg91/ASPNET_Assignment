using Business.Models;
using Data.Models;

namespace WebApp.ViewModels;

public class ProjectsViewModel
{
    public AddProjectForm AddProjectForm { get; set; } = new AddProjectForm();
    public EditProjectForm EditProjectForm { get; set; } = new EditProjectForm();
    public IEnumerable<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

    public bool ShowingCompleted { get; set; }
}
