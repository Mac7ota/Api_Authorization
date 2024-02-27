using Exodus.Cotacao.Authorization.Models;
using Exodus.Cotacao.Authorization.Services.Interfaces;
using Exodus.Cotacao.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Exodus.Cotacao.Authorization.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IConfiguration configuration, IUserService userService, ITokenService tokenService)
    {
        _configuration = configuration;
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Register(RegisterDTO registerDTO)
    {
        var user = new Models.Users.User()
        {
            Name = registerDTO.Name,
            Password = registerDTO.Password,
            ConfirmPassword = registerDTO.ConfirmPassword,
            Email = registerDTO.Email,
            ConfirmEmail = registerDTO.ConfirmEmail,
            Telephone = registerDTO.Telephone,
            CPF = registerDTO.CPF,
            Role = Models.Users.Role.User
        };

        _userService.Register(user);

        return Created();
    }

    [Authorize]
    [HttpGet("Check")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Check(string CPF)
    {
        var user = _userService.Check(CPF);

        if (user is not null)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Login(LoginDTO loginDTO)
    {
        var user = _userService.Login(loginDTO.CPForEmail, loginDTO.Password);

        if (user is not null)
        {
            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        return NotFound();
    }

    [Authorize]
    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(DeleteDTO deleteDTO)
    {
        var user = _userService.Delete(deleteDTO.Password, deleteDTO.ConfirmPassword);

        if (user is not null)
        {
            return Ok();
        }
        return BadRequest();
    }

}
