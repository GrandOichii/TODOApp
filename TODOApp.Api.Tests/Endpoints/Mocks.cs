
using AutoMapper;
using Microsoft.Extensions.Configuration;
using TODOApp.Api.Dtos;
using TODOApp.Api.Exceptions;
using TODOApp.Api.Models;

namespace TODOApp.Api.Tests;

public class UsersServiceMock : IUsersService
{
    public List<User> Users { get; } = new() {
    };

    readonly IMapper _mapper;
    readonly IConfiguration _config;
    

    public UsersServiceMock(IMapper mapper) {
        _mapper = mapper;


        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Users.Add(new() { 
            Username = "testuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
        });
    }

    public async Task<string> Login(CreateUser user)
    {
        var existing = Users.FirstOrDefault(u => u.Username == user.Username)
            ?? throw new TODOAPPApiBaseException();

        if (!BCrypt.Net.BCrypt.Verify(user.Password, existing.PasswordHash))
            throw new TODOAPPApiBaseException();

        // HAS to be the config file, can't just use a random string
        return AuthUtil.CreateToken(existing, _config.GetSection("AppSettings:Token").Value!);
    }

    public async Task<GetUser> Register(CreateUser newUser)
    {
        var existing = Users.FirstOrDefault(u => u.Username == newUser.Username);
        if (existing is not null) {
            throw new TODOAPPApiBaseException();
        }
        var result = new User() {
            Username = newUser.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password)
        };
        Users.Add(result);
        return _mapper.Map<GetUser>(result);
    }
}

public class UserTasksServiceMock : IUserTasksService
{
    readonly IUsersService _usersService;
    readonly IMapper _mapper;

    public UserTasksServiceMock(IUsersService usersService, IMapper mapper) {
        _usersService = usersService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetUserTask>> All(string username)
    {
        // TODO bad?
        var user = (_usersService as UsersServiceMock)!.Users.FirstOrDefault(u => u.Username == username)
            ?? throw new TODOAPPApiBaseException();

        return user.Tasks.Select(t => _mapper.Map<GetUserTask>(t));
    }

    public async Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask, string username)
    {
        // TODO bad?
        var user = (_usersService as UsersServiceMock)!.Users.FirstOrDefault(u => u.Username == username)
            ?? throw new TODOAPPApiBaseException();

        user.Tasks.Add(new() {
            Title = newUserTask.Title,
            Description = newUserTask.Description
        });

        return user.Tasks.Select(t => _mapper.Map<GetUserTask>(t));
    }
}