using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Infrastructure
{
    public class EFStormSessionRepository : IBrainstormSessionRepository
    {
        private readonly AppDbContext _dbContext;

        public EFStormSessionRepository(AppDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<List<BrainstormSession>> ListAsync()
        {
            var result = await _dbContext.BrainstormSessions
                .Include(s => s.Ideas)
                .ToListAsync();
            return result.OrderByDescending(x => x.DateCreated)
                               .ToList();
        }


        public Task AddAsync(BrainstormSession session)
        {
            _dbContext.BrainstormSessions.Add(session);
            return _dbContext.SaveChangesAsync();
        }


        public Task UpdateAsync(BrainstormSession session)
        {
            _dbContext.Entry(session).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
