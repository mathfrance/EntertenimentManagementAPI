using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using EntertenimentManager.API.Services;
using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Entities.Users;
using System.Security.Claims;

namespace EntertenimentManager.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(
            [FromBody] CreateAccountCommand command,
            [FromServices] AccountHandler handler,
            [FromServices] EmailService emailService)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível realizar o cadastro", ModelState.GetErrors()));

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;

                if (commandResult.Success)
                {
                    var login = (Login)commandResult.Data;

                    emailService.Send(
                        login.Name,
                        login.Email,
                        "Bem vindo!",
                        $"Sua senha é <strong>{login.Password}</strong>");
                }

                return Ok(commandResult);

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new GenericCommandResult(false, "Não foi possível realizar o cadastro", null));
            }
            catch
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            [FromServices] AccountHandler handler,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível realizar o login", ModelState.GetErrors()));

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                if (commandResult.Success)
                {
                    var token = tokenService.GenerateToken((User)commandResult.Data);
                    commandResult.Data = token;
                }
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("v1/accounts/allow-admin")]
        public async Task<IActionResult> AllowAdmin(
            [FromBody] AllowAdminCommand command,
            [FromServices] AccountHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível alterar a permissão", ModelState.GetErrors()));

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [Authorize]
        [HttpPost("v1/accounts/update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateAccountCommand command,
            [FromServices] AccountHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível alterar o usuário", ModelState.GetErrors()));
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            command.RequestEmail = identity.Name;
            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [Authorize]
        [HttpDelete("v1/accounts/")]
        public async Task<IActionResult> Delete(
            [FromServices] DeleteAccountCommand command,
            [FromServices] AccountHandler handler)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            command.Email = identity.Name;
            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }
    }
}
