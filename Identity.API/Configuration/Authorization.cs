namespace Identity.API.Configuration
{
    public static class Authorization
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
        {
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy("Admin", policy =>
            //    {
            //        policy.RequireClaim("Type", "High");

            //    });
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                     policy => policy.RequireRole("Admin"));
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
