namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddHealthChecks();

            var app = builder.Build();
            


            app.UseHealthChecks("/health");
            app.MapHealthChecks("/health");
            app.UseMiddleware<CorsMiddleware>();
            app.MapControllers();
            app.UseCors(a => a.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            );

            app.Run();
        }
    }

    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

            await _next(context);
        }
    }
}