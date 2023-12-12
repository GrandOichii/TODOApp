using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Services;

public interface IUsersService {
    public Task<GetUser> Register(CreateUser newUser);
    public Task<string> Login(CreateUser user);
}