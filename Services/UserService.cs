namespace rapid_news_media_auth_api.Services;
using Microsoft.EntityFrameworkCore;

using rapid_news_media_auth_api.Models;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User?> GetById(long id);
    Task<User?> Update(User user);
    Task<User> Create(User user);
    Task<int> Delete(User user);
}

public class UserService : IUserService
{
    private readonly AuthDBContext _context;

    public UserService(AuthDBContext context)
    {
        _context = context;
    }
    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> Update(User user)
    {
        if (!UserExists(user.Id))
        {
            return null;
        }

        try
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<int> Delete(User user)
    {
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync();
    }

    private bool UserExists(long id)
    {
        return _context.Users.Any(user => user.Id == id);
    }


}