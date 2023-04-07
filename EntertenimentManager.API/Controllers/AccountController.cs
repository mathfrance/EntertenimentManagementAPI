using EntertenimentManager.API.ViewModels;
using EntertenimentManager.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using EntertenimentManager.API.Services;
using EntertenimentManager.API.Data;
using EntertenimentManager.API.ViewModels.Accounts;
using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Enum;

namespace EntertenimentManager.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] EmailService emailService,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var password = PasswordGenerator.Generate();

            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == (int)EnumRoles.user);
            var user = new User(model.Name, model.Email, PasswordHasher.Hash(password), "");
            user.AddRole(role);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                emailService.Send(
                    user.Name,
                    user.Email,
                    "Bem vindo!",
                    "Sua senha é <strong>{password}</strong>");

                var login = new LoginViewModel
                {
                    Email = user.Email,
                    Password = password
                };

                return Ok(new ResultViewModel<LoginViewModel>(login));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("Não foi possível cadastrar este e-mail"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] EntertenimentManagementDataContext context,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null) return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch (Exception)
            {

                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("v1/accounts/setup-admin")]
        public async Task<IActionResult> SetupAdmin(
            [FromBody] EditUserViewModel model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null) return StatusCode(401, new ResultViewModel<string>("Usuário não encontrado"));

            try
            {
                //user.AddRoles(await context.Roles.ToListAsync());
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<string>("Permissões de administrador concedidas ao usuário"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [Authorize]
        [HttpPost("v1/accounts/upload-image")]
        public async Task<IActionResult> UploadImage(
        [FromBody] UploadImageViewModel model,
        [FromServices] EntertenimentManagementDataContext context)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");
            var bytes = Convert.FromBase64String(data);

            try
            {
                await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{fileName}", bytes);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }

            var user = await context
                .Users
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if (user == null)
                return NotFound(new ResultViewModel<string>("Usuário não encontrado"));

            //user.UpdateImage($"https://localhost:5000/images/{fileName}");
            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }

            return Ok(new ResultViewModel<string>("Imagem alterada com sucesso.", null));
        }
    }
}
