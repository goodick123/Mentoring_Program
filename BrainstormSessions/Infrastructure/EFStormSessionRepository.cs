using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BrainstormSessions.Infrastructure
{
    public class EFStormSessionRepository : IBrainstormSessionRepository
    {
        private readonly AppDbContext _dbContext;

        public EFStormSessionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            Log.Logger.Information($"Requested to get session with id: {id}.");
            return _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<BrainstormSession>> ListAsync()
        {
            Log.Logger.Information("Requested to get list of sessions.");
            return _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .OrderByDescending(s => s.DateCreated)
                .ToListAsync();
        }

        public Task AddAsync(BrainstormSession session)
        {
            Log.Logger.Information($"Requested adding session with id: {session.Id} to DB.");
            _dbContext.BrainstormSessions.Add(session);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            Log.Logger.Information($"Called session update with session id: {session.Id}.");
            _dbContext.Entry(session).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
