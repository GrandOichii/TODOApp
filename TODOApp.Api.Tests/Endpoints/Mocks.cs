
using AutoMapper;
using TODOApp.Api.Dtos;
using TODOApp.Api.Exceptions;
using TODOApp.Api.Models;

namespace TODOApp.Api.Tests;

public class UsersServiceMock : IUsersService
{
    private List<User> _users = new() {

    };

    private IMapper _mapper;

    public UsersServiceMock(IMapper mapper) {
        _mapper = mapper;
    }

    public async Task<string> Login(CreateUser user)
    {
        var existing = _users.FirstOrDefault(u => u.Username == user.Username)
            ?? throw new TODOAPPApiBaseException();

        if (!BCrypt.Net.BCrypt.Verify(user.Password, existing.PasswordHash))
            throw new TODOAPPApiBaseException();

        return AuthUtil.CreateToken(existing, "JWT key for testing");
    }

    public async Task<GetUser> Register(CreateUser newUser)
    {
        var existing = _users.FirstOrDefault(u => u.Username == newUser.Username);
        if (existing is not null) {
            throw new TODOAPPApiBaseException();
        }
        var result = new User() {
            Username = newUser.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password)
        };
        _users.Add(result);
        return _mapper.Map<GetUser>(result);
    }
}

// public class UserTasksServiceMock : IUserTasksService
// {
//     // List<UserTask> _tasks = new() {
//     //     new() { Title = "Task1"}
//     // };
//     public UserTasksServiceMock() {
//     }

//     public Task<IEnumerable<GetUserTask>> All(string username)
//     {
//         throw new NotImplementedException();

//     }

//     public Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask, string username)
//     {
//         throw new NotImplementedException();
//     }
// }