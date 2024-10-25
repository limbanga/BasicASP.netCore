using Microsoft.AspNetCore.Identity;

namespace ClothesStore.Models
{
    public class ApplicationUserz :  IdentityUser<Guid>
    {
        public string HashedPassword { get; set; } = "";
    }
}
