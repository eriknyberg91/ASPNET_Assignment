using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[Route("projects")]
public class ProjectsController : Controller
{
    [HttpPost]
    public IActionResult AddProject(AddProjectForm form)
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
        //var result = await _clientService.AddClientAsync(form);
        return Ok();


    }

    [HttpPost]
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
}
