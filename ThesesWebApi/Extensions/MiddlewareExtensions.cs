using Microsoft.EntityFrameworkCore;

namespace ThesesWebApi.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
