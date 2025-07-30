using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Brief_Bibliotheque.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthApiController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Inscription d'un utilisateur
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Inscrire un nouvel utilisateur")]
        [SwaggerResponse(200, "Utilisateur inscrit")]
        [SwaggerResponse(400, "Erreur d'inscription")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return Ok("Inscription réussie");

            return BadRequest(result.Errors);
        }

        // Connexion
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Connecter un utilisateur")]
        [SwaggerResponse(200, "Connexion réussie")]
        [SwaggerResponse(401, "Connexion échouée")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
                return Ok("Connexion réussie");

            return Unauthorized("Email ou mot de passe incorrect");
        }

        // Déconnexion
        [HttpPost("logout")]
        [SwaggerOperation(Summary = "Déconnecter l'utilisateur actuel")]
        [SwaggerResponse(200, "Déconnexion réussie")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Déconnexion réussie");
        }
    }
}
