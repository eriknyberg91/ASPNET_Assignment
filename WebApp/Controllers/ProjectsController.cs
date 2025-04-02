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
            ProjectName = form.ProjectName
        };

        await _projectService.CreateAsync(entity);


        return CreatedAtAction(nameof(GetProject), new { id = entity.Id }, entity);
    }

    [HttpPost("edit")]
    public IActionResult EditProject(AddProjectForm form)
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

        //TODO: Send data to clientService/database
        //var result = await _clientService.UpdateClientAsync(form);
        return Ok();

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
