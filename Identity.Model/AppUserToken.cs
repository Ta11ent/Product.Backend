using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppUserToken : IdentityUserToken<string>
    {
        public string Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
