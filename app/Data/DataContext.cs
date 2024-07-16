using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
    }
}