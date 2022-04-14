namespace rapid_news_media_auth_api.Services;

using rapid_news_media_auth_api.Models;

public interface IAuthService
{
    Task<User> Login(string username, string password);
    void Logout();
    Task<IEnumerable<User>> GetAll();
}

public class AuthService : IAuthService
{
    private IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        new User { Id = 1, Firstname = "John", Lastname = "Smith", Username = "john", Password = "test", AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dnZWRJbkFzIjoiYWRtaW4iLCJpYXQiOjE0MjI3Nzk2Mzh9.gzSraSYS8EXBxLN_oWnFSRgCzcmJmMjLiuyu5CSpyHI" },
        new User { Id = 1, Firstname = "Erika", Lastname = "Mullen", Username = "erika", Password = "test", AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dnZWRJbkFzIjoiYWRtaW4iLCJpYXQiOjE0MjI3Nzk2Mzh9.gzSraSYS8EXBxLN_oWnFSRgCzcmJmMjLiuyu5CSpyHI" },
        new User { Id = 1, Firstname = "Jessica", Lastname = "Luarez", Username = "jessica", Password = "test", AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dnZWRJbkFzIjoiYWRtaW4iLCJpYXQiOjE0MjI3Nzk2Mzh9.gzSraSYS8EXBxLN_oWnFSRgCzcmJmMjLiuyu5CSpyHI" }
    };

    public async Task<User> Login(string username, string password)
    {
        // wrapped in "await Task.Run" to mimic fetching user from a db
        var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public void Logout()
    { 
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        // wrapped in "await Task.Run" to mimic fetching users from a db
        return await Task.Run(() => _users);
    }
}