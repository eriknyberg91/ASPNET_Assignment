using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;

public class AddProjectForm
{

    public AddProjectForm()
    {
        StartDate = DateTime.Today;
    }

    [Required]
    [Display(Name = "Project Name", Prompt = "Enter project name")]
    public string ProjectName { get; set; } = null!;

    [Required]
    [Display(Name = "Client Name", Prompt = "Enter client name")]
    public string ClientName { get; set; } = null!;

    [Required]
    [Display(Name = "Description", Prompt = "Enter project description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Is Completed")]
    public bool IsCompleted { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Budget")]
    public decimal? Budget { get; set; }
}
