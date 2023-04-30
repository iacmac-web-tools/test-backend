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

            var app = builder.Build();
            app.UseCors(a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
          



            app.MapControllers();

            app.Run();
        }
    }
}