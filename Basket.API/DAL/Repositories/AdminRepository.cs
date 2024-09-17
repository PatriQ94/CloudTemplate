using Basket.API.BO.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.DAL.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly DBContext _context;
    private readonly ILogger<AdminRepository> _logger;

    public AdminRepository(DBContext context, ILogger<AdminRepository> logger)
    {
        _context = context;
        _logger = logger;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task CreateDatabase()
    {
        //Check if the database exists
        if (!_context.Database.CanConnect())
        {
            //Create the database and tables
            try
            {
                var conn = _context.Database.GetConnectionString();
                _logger.LogInformation($"Creating postgres database");
                await _context.Database.MigrateAsync();
                _logger.LogInformation($"Created postgres database");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Failed to create Basket database: {ex}");
                throw new Exception($"Failed to create Basket database");
            }
        }

        if ((await _context.Database.GetPendingMigrationsAsync()).Any())
        {
            _logger.LogInformation($"Applying postgres migrations");
            await _context.Database.MigrateAsync();
            _logger.LogInformation($"Finished applying postgres migrations");
        }
        else
        {
            _logger.LogInformation($"All database migrations already applied, skipping...");
        }
    }
}
