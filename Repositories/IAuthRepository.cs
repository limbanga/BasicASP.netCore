using ClothesStore.Models;

namespace ClothesStore.Repositories
{
    public interface IAuthRepository
    {
        Task<ApplicationUserz> LoginAsync(string email, string password);
    }
}
