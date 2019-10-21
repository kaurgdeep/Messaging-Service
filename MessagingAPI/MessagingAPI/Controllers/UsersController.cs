﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MessagingAPI.Dtos;
using MessagingAPI.Interfaces.Services;
using MessagingAPI.Models;
using MessagingAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MessagingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : MessagingBaseController
    {
        private IConfiguration Configuration;
        private IEntityService<User> UserService;
        private IEntityService<UserFriend> UserFriendService;



        public UsersController(IConfiguration configuration, IEntityService<User> userService, IEntityService<UserFriend> userFriendService)
        {
            Configuration = configuration;
            UserService = userService;
            UserFriendService = userFriendService;

        }

        // POST api/values
        [HttpPost]
        [Route("register")]
        public ActionResult<TokenResponse> Register([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                // todo: Hash the password before saving
                var user = new User
                {
                    EmailAddress = userDto.EmailAddress.ToLower(),
                    PasswordHash = userDto.Password,
                    Name = userDto.Name,
                    Created = DateTime.UtcNow
                };
                var userId = UserService.Create(user);
                user.UserId = userId;
                var token = CreateToken(user);

                return Ok(new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                // TODO: Log the exception details 
                var messages = new List<string>();
                while (ex != null)
                {
                    messages.Add(ex.Message);
                    ex = ex.InnerException;
                }

                // Note: These are 2 different ways to return an error result
                // JsonResult is better because we can send back error information in JSON (just like success)
                // which makes handling any HTTP calls to API uniform for the clients of the API (like the Web)
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Error = string.Join("\r\n", messages) });
                // Note: In production, don't return any more details - return just the status
                //return StatusCode(StatusCodes.Status500InternalServerError, string.Join("\r\n", messages));
            }
        }

        [HttpPost]
        [Route("log-in")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginUserDto userDto)
        {
            // todo: hash the input password and check with passwordhash from database
            var user = UserService.Get(u =>
                u.EmailAddress == userDto.EmailAddress.ToLower() &&
                u.PasswordHash == userDto.Password);

            if (user == null)
            {
                return BadRequest("Could not verify username and password");
            }

            var token = CreateToken(user);

            return Ok(new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetUser([FromQuery]int skip = 0, [FromQuery]int take = 25)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }


            var usersList = UserService.GetMany(x => x.UserId != LoggedInUserId, skip, take);

            return Ok(usersList);

        }

        [HttpPost]
        [Route("friend/{friendId}")]
        public ActionResult CreateAddFriend(int friendId)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }

            var userFriend = new UserFriend
            {
                UserId = LoggedInUserId.Value,

                FriendId = friendId


            };
            UserFriendService.Create(userFriend);


            return Ok();

        }

        [HttpDelete]
        [Route("friend/{friendId}")]
        public ActionResult Delete(int friendId)
        {
            UserFriendService.Delete(x => x.FriendId == friendId && x.UserId == LoggedInUserId);

            return Ok();

        }

        private JwtSecurityToken CreateToken(User user)
        {
            var claims = new[] {
                new Claim(Constants.EmailAddress, user.EmailAddress),
                new Claim(Constants.UserId, user.UserId.ToString()),
                new Claim(Constants.Name, user.Name),
                

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JWTSecurityKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MessagingService.com",
                audience: "MessagingService.com",
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return token;
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public ActionResult Me()
        {
            return Ok(new MeResponse
            {
                UserId = LoggedInUserId,
                EmailAddress = LoggedInEmailAddress,
                Name = LoggedInName,
                

            });
        }

    }
}