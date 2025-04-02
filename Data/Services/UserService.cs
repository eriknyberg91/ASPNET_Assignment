using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace Data.Services;

public class UserService(UserManager<AppUser> userManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<bool> CreateAsync(UserSignUpForm form)
    {
        if (await _userManager.Users.AnyAsync(u => u.Email == form.Email)) 
        return false;

        var appUser = new AppUser
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            UserName = form.Email,
            PhoneNumber = form.Phone
        };

        var result = await _userManager.CreateAsync(appUser, form.Password);
        if (result.Succeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
