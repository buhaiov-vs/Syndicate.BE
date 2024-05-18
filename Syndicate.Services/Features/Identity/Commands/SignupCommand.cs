using Microsoft.AspNetCore.Identity;
using Syndicate.Data.Models;
using Syndicate.Services.Extensions;
using Syndicate.Services.Features.Identity.Models.Requests;
using Syndicate.Services.Features.Identity.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Identity.Commands;

public class SignupCommand(UserManager<User> _userManager, SignInManager<User> _signInManager)
{
    public async Task<ApiResponse<SignupResponse>> ExecuteAsync(SignupRequest request)
    {
        var newUser = new User()
        {
            Email = request.Email,
            Name = request.Name ?? request.Email.Split("@")[0],
            UserName = request.Username,
            Type = request.Type,
        };

        var result = await _userManager.CreateAsync(
            newUser,
            request.Password
        );

        if (result == null)
        {
            return new(HttpStatusCode.BadRequest, "User was not created, please try later");
        }
        else if (result.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, false);

            return new(new() { UserId = newUser.Id });
        }

        return result.Errors.Empty()
            ? throw new InvalidOperationException("Something went wrong. Not able to signup.")
            : new(HttpStatusCode.BadRequest, result.Errors.First().Description);
    }
}
