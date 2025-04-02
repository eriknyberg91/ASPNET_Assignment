using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;

public class AddProjectForm
{
    //TODO: Add fields for Projects

    [Required]
    public string ProjectName { get; set; } = null!;


}
