
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TODOApp.Api.Services;

public class UsersService : IUsersService
{
    DataContext _ctx;
    readonly IMapper _mapper;
    readonly IConfiguration _config;
    
    public UsersService(DataContext ctx, IMapper mapper, IConfiguration config) {
        _ctx = ctx;
        _mapper = mapper;
        _config = config;
    }

    public async Task<string> Login(CreateUser user)
    {
        var existingUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Username == user.Username) 
            ?? throw new TODOAPPApiBaseException("Incorrect username/password");

        var correct = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.PasswordHash);
        if (!correct) throw new TODOAPPApiBaseException("Incorrect username/password");

        return AuthUtil.CreateToken(existingUser, _config.GetSection("AppSettings:Token").Value!);
    }

    public async Task<GetUser> Register(CreateUser newUser)
    {
        var withSameUsername = await _ctx.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username);

        if (withSameUsername is not null) {
            throw new TODOAPPApiBaseException("Username " + newUser.Username + " is already taken");
        }

        var passHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        var result = new User(){
            Username = newUser.Username,
            PasswordHash = passHash
        };

        await _ctx.Users.AddAsync(result);
        await _ctx.SaveChangesAsync();

        return _mapper.Map<GetUser>(result);
    }
}