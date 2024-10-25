using ClothesStore.Models;

namespace ClothesStore.Repositories.implements
{
    public class AuthRepository : IAuthRepository
    {
        Task<ApplicationUserz> IAuthRepository.LoginAsync(string email, string password)
        {
            return Task.FromResult<ApplicationUserz>(new ApplicationUserz());
        }
    }
}
