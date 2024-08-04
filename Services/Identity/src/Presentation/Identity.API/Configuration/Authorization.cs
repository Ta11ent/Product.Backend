namespace Identity.API.Configuration
{
    public static class Authorization
    {
        public static IServiceCollection _AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                     policy => policy.RequireRole("Admin"));
                options.AddPolicy("User",
                     policy => policy.RequireRole("USER"));
            });

            //    services.AddAuthorizationBuilder()
            //    .AddPolicy("Admin", policy =>
            //policy
            //    .RequireRole("Admin"));
            // .RequireClaim("scope", "greetings_api"));


            return services;
        }
    }
}
