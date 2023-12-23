namespace Identity.Domain
{
    internal class AspNetUserRoles
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }

    }
}
