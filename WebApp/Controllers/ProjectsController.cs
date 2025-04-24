using Business.Models;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[Route("projects")]
public class ProjectsController(ProjectService projectService) : Controller
{

    private readonly ProjectService _projectService = projectService;

    [HttpPost("create")]
    [Route("admin/projects")]
    public async Task<IActionResult> CreateProjectAsync(AddProjectForm form)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new { errors });
        }

        if (string.IsNullOrWhiteSpace(form.ProjectName))
        {
            return BadRequest("Project name cannot be empty.");
        }

        var existingProject = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);
        if (existingProject != null)
        {
            return Conflict("A project with this name already exists.");
        }

        ProjectEntity entity = new()
        {
            ProjectName = form.ProjectName,
            ClientName = form.ClientName,
            Description = form.Description,
            IsCompleted = false,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget
        };

        await _projectService.CreateAsync(entity);


        return CreatedAtAction(nameof(GetProject), new { id = entity.Id }, entity);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> EditProject(EditProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        var formCheck = form;

        var projectToUpdate = await _projectService.GetProjectAsync(x => x.Id == form.Id);
        if (projectToUpdate == null)
        {
            return NotFound(new { success = false, message = "Project not found" });
        }

        var result = await _projectService.UpdateProjectAsync(projectToUpdate.Id, form);
        return Ok(new { success = true });
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectEntity>> GetAll()
    {
        return await _projectService.GetAllProjectsAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var customer = await _projectService.GetProjectAsync(x => x.Id == id);
        if (customer != null)
        {
            return Ok(customer);
        }
        return NotFound();
    }
}
