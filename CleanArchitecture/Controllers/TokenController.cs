using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace WebUI.Controllers;

public class TokenController : ApiControllerBase
{
    private readonly IOpenIddictApplicationManager _applicationManager;

    public TokenController(IOpenIddictApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }

    [HttpPost(Name = nameof(Exchange))]
    public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
    {
        if (!request.IsPasswordGrantType())
        {
            throw new NotImplementedException("The specified grant is not implemented.");
        }

        // Note: the client credentials are automatically validated by OpenIddict:
        // if client_id or client_secret are invalid, this action won't be invoked.

        var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
            throw new InvalidOperationException("The application cannot be found.");

        // Create a new ClaimsIdentity containing the claims that
        // will be used to create an id_token, a token or a code.
        var identity = new ClaimsIdentity(TokenValidationParameters.DefaultAuthenticationType, Claims.Name, Claims.Role);

        // Use the client_id as the subject identifier.
        identity.SetClaim(Claims.Subject, await _applicationManager.GetClientIdAsync(application));
        identity.SetClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application));

        identity.SetDestinations(static claim => claim.Type switch
        {
            // Allow the "name" claim to be stored in both the access and identity tokens
            // when the "profile" scope was granted (by calling principal.SetScopes(...)).
            Claims.Name when claim.Subject.HasScope(Scopes.Profile)
                => new[] { Destinations.AccessToken, Destinations.IdentityToken },

            // Otherwise, only store the claim in the access tokens.
            _ => new[] { Destinations.AccessToken }
        });

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private static void AddRolesToPrincipal(ClaimsPrincipal principal, string[] roles)
    {
        var identity = principal.Identity as ClaimsIdentity;

        var alreadyHasRolesClaim = identity.Claims.Any(c => c.Type == "role");
        if (!alreadyHasRolesClaim && roles.Any())
        {
            identity.AddClaims(roles.Select(r => new Claim("role", r)));
        }

        var newPrincipal = new System.Security.Claims.ClaimsPrincipal(identity);
    }
}
