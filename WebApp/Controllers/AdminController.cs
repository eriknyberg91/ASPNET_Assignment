using Business.Models;
using Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;
[Authorize]
[Route("admin")]
public class AdminController(ProjectService projectService) : Controller
{

    private readonly ProjectService _projectService = projectService;
    [Route("projects")]
    [HttpGet("projects")]
    public async Task<IActionResult> Projects(bool showCompleted = false)
    {
        var projects = await _projectService.GetAllProjectsAsync();
        var filtered = projects.Where(p => p.IsCompleted == showCompleted);

        var viewModel = new ProjectsViewModel
        {
            Projects = filtered,
            ShowingCompleted = showCompleted
        };

        return View(viewModel);
    }



}
