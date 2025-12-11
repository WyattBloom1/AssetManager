using Microsoft.AspNetCore.Identity;

namespace AssetManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid Id { get; set; }
        // more user related properties like name, cellphone etc

    }
}