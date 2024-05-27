using Microsoft.AspNetCore.Identity;
using Syndicate.Data.Models.Identity;
using Syndicate.Services.Features.Identity.Models.Requests;
using Syndicate.Services.Features.Identity.Models.Responses;
using System.Net;

namespace Syndicate.Services.Features.Identity.Queries;

public class LoginQuery(UserManager<User> _userManager, SignInManager<User> _signInManager)
{
    public async Task<ApiResponse<LoginResponse>> ExecuteAsync(LoginRequest request)
    {
        var loginResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, false);

        if (loginResult.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            return new(new() { UserId = user!.Id });
        }
        else if (loginResult.RequiresTwoFactor)
        {
            return new(HttpStatusCode.Unauthorized, "2 Factor Authentication required", new() { Requires2FA = true });
        }
        else if (loginResult.IsLockedOut)
        {
            return new(HttpStatusCode.Unauthorized, "Account is locked", new() { IsLocked = true });
        }

        return new(HttpStatusCode.Unauthorized, "Invalid credentials");
    }
}
