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
using Application.Common.Exceptions;

namespace Tests;

public class Tests
{
    private MockUserRepository _userRepository;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _userRepository = new MockUserRepository();
        _cancellationToken = CancellationToken.None;
    }

    [Test]
    public async Task CreateUserCommandHandler_Success()
    {
        var handler = new CreateUserCommandHandler(_userRepository);
        var command = new CreateUserCommand
        {
            Name = "John",
            Birthday = new DateOnly(1980, 01, 01),
            CreatedBy = "Admin",
            Gender = 0,
            IsAdmin = false,
            Login = "John123",
            Password = "123456abc"
        };

        var result = await handler.Handle(command, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(_userRepository._data.Count, Is.EqualTo(2));
        Assert.That(_userRepository._data.Any(u => u.Login == "John123"), Is.True);
    }

    [Test]
    public async Task GetActiveUsersQueryHandler_Success()
    {
        var handler = new GetActiveUsersQueryHandler(_userRepository);
        var query = new GetActiveUsersQuery();

        var result = await handler.Handle(query, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Login, Is.EqualTo("Admin"));
    }

    [Test]
    public async Task GetUsersOverTheAgeQueryHandler_Success()
    {
        var handler = new GetUsersOverTheAgeQueryHandler(_userRepository);
        var query = new GetUsersOverTheAgeQuery { Age = 40 };

        var result = await handler.Handle(query, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Login, Is.EqualTo("Admin"));
    }

    [Test]
    public async Task GetUserByLoginQueryHandler_Success()
    {
        var handler = new FindUserByLoginQueryHandler(_userRepository);
        var query = new FindUserByLoginQuery { Login = "Admin", QueryBy = "Admin" };

        var result = await handler.Handle(query, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(result.Login, Is.EqualTo("Admin"));
    }

    [Test]
    public async Task GetUserByLoginAndPasswordQueryHandler_Success()
    {
        var handler = new FindUserByLoginAndPasswordQueryHandler(_userRepository);
        var query = new FindUserByLoginAndPasswordQuery { Login = "Admin", Password = "admin", QueryBy = "Admin" };

        var result = await handler.Handle(query, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(result.Login, Is.EqualTo("Admin"));
    }

    [Test]
    public async Task UpdateUserCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new UpdateUserCommandHandler(_userRepository);
        var command = new UpdateUserCommand
        {
            Login = adminUser.Login,
            Name = "New Admin Name",
            Birthday = new DateOnly(1981, 01, 01),
            ModifiedBy = "Admin",
            Gender = 1,
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().Name, Is.EqualTo("New Admin Name"));
        Assert.That(_userRepository._data.First().Birthday, Is.EqualTo(new DateOnly(1981, 01, 01)));
        Assert.That(_userRepository._data.First().Gender, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateLoginCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new UpdateLoginCommandHandler(_userRepository);
        var command = new UpdateLoginCommand
        {
            CurrentLogin = adminUser.Login,
            NewLogin = "newadminlogin123",
            ModifiedBy = "Admin",
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().Login, Is.EqualTo("newadminlogin123"));
    }

    [Test]
    public async Task UpdatePasswordCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new UpdatePasswordCommandHandler(_userRepository);
        var command = new UpdatePasswordCommand
        {
            Login = adminUser.Login,
            Password = "newadminpassword123",
            ModifiedBy = "Admin",
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().Password, Is.EqualTo("newadminpassword123"));
    }

    [Test]
    public async Task RevokeUserCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new RevokeUserCommandHandler(_userRepository);
        var command = new RevokeUserCommand
        {
            Login = adminUser.Login,
            RevokeBy = "Admin",
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().RestoredOn, Is.Not.Null);
    }

    [Test]
    public async Task RestoreUserCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();

        var revokeHandler = new RevokeUserCommandHandler(_userRepository);
        var revokeCommand = new RevokeUserCommand
        {
            Login = adminUser.Login,
            RevokeBy = "Admin",
        };
        await revokeHandler.Handle(revokeCommand, _cancellationToken);

        var handler = new RestoreUserCommandHandler(_userRepository);
        var command = new RestoreUserCommand
        {
            Login = adminUser.Login,
            RestoreBy = "Admin",
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().RestoredOn, Is.Null);
    }

    [Test]
    public async Task DeleteUserCommandHandler_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new DeleteUserCommandHandler(_userRepository);
        var command = new DeleteUserCommand
        {
            Login = adminUser.Login,
            DeleteBy = "Admin",
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task CreateUserCommandHandler_DuplicateLogin_ThrowsException()
    {
        var handler = new CreateUserCommandHandler(_userRepository);
        var command = new CreateUserCommand
        {
            Name = "John",
            Birthday = new DateOnly(1980, 01, 01),
            CreatedBy = "Admin",
            Gender = 0,
            IsAdmin = false,
            Login = "Admin",
            Password = "123456abc"
        };

        Assert.ThrowsAsync<AlreadyCreatedException>(async () => await handler.Handle(command, _cancellationToken));
    }

    [Test]
    public async Task GetUserByLoginQueryHandler_NotFound()
    {
        var handler = new FindUserByLoginQueryHandler(_userRepository);
        var query = new FindUserByLoginQuery { Login = "nonexistentuser", QueryBy = "Login" };

        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, _cancellationToken));
    }

    [Test]
    public async Task UpdateUserCommandHandler_NotFound_ThrowsException()
    {
        var handler = new UpdateUserCommandHandler(_userRepository);
        var command = new UpdateUserCommand
        {
            Login = "noUser",
            Name = "NewAdminName",
            Birthday = new DateOnly(1981, 01, 01),
            ModifiedBy = "Admin",
            Gender = 1,
        };

        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, _cancellationToken));
    }

    [Test]
    public async Task CreateUserCommandHandler_NullBirthday_Success()
    {
        var handler = new CreateUserCommandHandler(_userRepository);
        var command = new CreateUserCommand
        {
            Name = "John",
            Birthday = null,
            CreatedBy = "Admin",
            Gender = 0,
            IsAdmin = false,
            Login = "John123",
            Password = "123456abc"
        };

        var result = await handler.Handle(command, _cancellationToken);

        Assert.IsNotNull(result);
        Assert.That(_userRepository._data.Count, Is.EqualTo(2));
        Assert.That(_userRepository._data.Any(u => u.Login == "John123"), Is.True);
    }

    [Test]
    public async Task UpdateUserCommandHandler_NullBirthday_Success()
    {
        var adminUser = _userRepository._data.First();
        var handler = new UpdateUserCommandHandler(_userRepository);
        var command = new UpdateUserCommand
        {
            Login = adminUser.Login,
            Name = "NewAdminName",
            Birthday = null,
            ModifiedBy = "Admin",
            Gender = 1,
        };

        await handler.Handle(command, _cancellationToken);

        Assert.That(_userRepository._data.First().Name, Is.EqualTo("NewAdminName"));
        Assert.That(_userRepository._data.First().Birthday, Is.Null);
        Assert.That(_userRepository._data.First().Gender, Is.EqualTo(1));
    }
}
