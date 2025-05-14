using System.ComponentModel.DataAnnotations;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.RecoverUser;
using Application.Users.Commands.RevokeUser;
using Application.Users.Commands.UpdateLogin;
using Application.Users.Commands.UpdatePassword;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.FindUserByLogin;
using Application.Users.Queries.FindUserByLoginAndPassword;
using Application.Users.Queries.GetActiveUsers;
using Application.Users.Queries.GetUsersOverTheAge;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;


namespace WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class UserController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllActiveUsers()
    {
        var query = new GetActiveUsersQuery();
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllOverTheAgeUsers([FromQuery, Required] int age)
    {
        var query = new GetUsersOverTheAgeQuery()
        {
            Age = age
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
    
    [HttpGet]
    public async Task<ActionResult<User>> GetUserByLogin([FromQuery, Required]  string login, [FromQuery, Required]  string queryBy)
    {
        var query = new FindUserByLoginQuery
        {
            Login = login,
            QueryBy = queryBy
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
    
    [HttpGet]
    public async Task<ActionResult<User>> GetUserByLoginAndPassword
    (
        [FromQuery, Required]  string login,
        [FromQuery, Required]  string password,
        [FromQuery, Required]  string queryBy
    )
    {
        var query = new FindUserByLoginAndPasswordQuery()
        {
            Login = login,
            Password = password,
            QueryBy = queryBy
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUser([FromQuery] CreateUserDto user)
    {
        var command = new CreateUserCommand
        {
            Name = user.Name,
            Birthday = user.Birthday,
            CreatedBy = user.CreatedBy,
            Gender = user.Gender,
            IsAdmin = user.IsAdmin,
            Password = user.Password,
            Login = user.Login
        };
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromQuery] UpdateUserDto user)
    {
        var command = new UpdateUserCommand
        {
            Login = user.Login,
            Name = user.Name,
            Birthday = user.Birthday,
            ModifiedBy = user.ModifiedBy,
            Gender = user.Gender,
        };
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateLogin
    (
        [FromQuery, Required]  string currentLogin,
        [FromQuery, Required]  string newLogin,
        [FromQuery, Required]  string modifiedBy
    )
    {
        var command = new UpdateLoginCommand
        {
            CurrentLogin = currentLogin,
            NewLogin = newLogin,
            ModifiedBy = modifiedBy,
            
        };
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdatePussword
    (
        [FromQuery, Required]  string login,
        [FromQuery, Required]  string pussword,
        [FromQuery, Required]  string modifiedBy
    )
    {
        var command = new UpdatePasswordCommand
        {
            Login = login,
            Password = pussword,
            ModifiedBy = modifiedBy,
        };
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> RevokeUser
    (
        [FromQuery, Required] string login,
        [FromQuery, Required] string revokeBy
    )
    {
        var command = new RevokeUserCommand
        {
            Login = login,
            RevokeBy = revokeBy,
        };
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    public async Task<ActionResult> RestoreUser
    (
        [FromQuery, Required] string login,
        [FromQuery, Required] string restoreBy
    )
    {
        var command = new RestoreUserCommand
        {
            Login = login,
            RestoreBy = restoreBy,
        };
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser
    (
        [FromQuery, Required] string login,
        [FromQuery, Required] string deleteBy
    )
    {
        var command = new DeleteUserCommand
        {
            Login = login,
            DeleteBy = deleteBy
        };
        await Mediator.Send(command);
        return NoContent();
    }
}