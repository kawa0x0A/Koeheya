using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Identity;

namespace Koeheya.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TwitterController : ControllerBase
    {
        private SignInManager<Data.ApplicationUser> signInManager;

        public TwitterController(SignInManager<Data.ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet("TwitterSignIn")]
        public async Task TwitterSignIn()
        {
            await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme, new AuthenticationProperties() { RedirectUri = "Twitter/TwitterLogin" });
        }

        [HttpGet("TwitterLogin")]
        public async Task<IActionResult> TwitterLogin()
        {
            using var applicationDbContext = new Data.ApplicationDbContext();

            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            var userId = authenticateResult.Principal!.Claims.Single(claim => claim.Type == "urn:twitter:userid").Value;
            var user = applicationDbContext.ApplicationUsers?.SingleOrDefault(u => u.UserId.ToString() == userId);

            if (user == null)
            {
                await applicationDbContext.ApplicationUsers!.AddAsync(new Data.ApplicationUser() { UserId = int.Parse(userId), UserName = authenticateResult.Principal.Identity?.Name });

                await applicationDbContext.SaveChangesAsync();

                user = applicationDbContext.ApplicationUsers.Single();
            }

            if(user.UserName != authenticateResult.Principal.Identity?.Name)
            {
                applicationDbContext.ApplicationUsers!.Update(user);

                await applicationDbContext.SaveChangesAsync();
            }

            await signInManager.ExternalLoginSignInAsync(authenticateResult.Principal.Identity!.AuthenticationType, user.UserId.ToString(), false);

            return Redirect($"/{user.UserId}");
        }
    }
}
