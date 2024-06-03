﻿using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IUserService userService,
            IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        #region Login
        /// <summary>
        /// Log into system using username and password
        /// </summary>    
        /// <remarks>
        ///     Sample request:
        ///
        ///         {
        ///           "username": "testingaccount",
        ///           "password": "test"
        ///         }
        ///         
        /// </remarks>
        /// <returns>Specific HTTP Status code</returns>
        /// <response code="200">Return home screen if the access is successful</response>
        /// <response code="400">If the account is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserSignInRequest userSignInRequest)
        {
            try
            {
                var result = await _userService.LoginAsync(
                    userSignInRequest.Username
                    , userSignInRequest.Password);

                if (result != null)
                {
                    var accessToken = GenerateAccessToken(result);
                    return Ok(accessToken);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return BadRequest(new
            {
                ErrorMessage = "Wrong UserName or Password"
            });
        }
        #endregion

        #region Generate Access Token
        private string GenerateAccessToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim("Id", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Key").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion
    }
}
