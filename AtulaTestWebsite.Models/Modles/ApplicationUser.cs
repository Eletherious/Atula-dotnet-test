using Microsoft.AspNetCore.Identity;

namespace AtulaTestWebsite.Models.Modles
{
    public class ApplicationUser : IdentityUser // This is to extend the identity system
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
