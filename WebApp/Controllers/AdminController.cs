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
    public async Task<IActionResult> Projects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        var viewModel = new ProjectsViewModel
        {
            Projects = projects
        };
        return View(viewModel);
    }



}
