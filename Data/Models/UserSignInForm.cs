﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;

public class UserSignInForm
{
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email Address", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = null!;
}
