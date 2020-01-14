using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using VinarishLib.Models;

namespace VinarishLib.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly VinarishDbContext _context;

        public DatabaseInitializer(VinarishDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //await _context.Database.EnsureCreatedAsync();// https://github.com/aspnet/EntityFrameworkCore/issues/2874
            //_context.GetService<IRelationalDatabaseCreator>().CreateTables();

            await _context.Database.MigrateAsync().ConfigureAwait(false);

            await _context.SaveChangesAsync();
        }
    }
}