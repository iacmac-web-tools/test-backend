using Microsoft.EntityFrameworkCore;

namespace ThesesWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    }
}
