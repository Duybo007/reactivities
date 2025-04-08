using Domain;

namespace Application.Interfaces;

public interface IUseAccessor
{
    string GetUserId();
    Task<User> GetUserAsync();
}
