using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<TBaseEntity> : IRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<List<TBaseEntity>> GetAll()
        {
            return await _context.Set<TBaseEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TBaseEntity> GetByIdAsync(int Id)
        {
            return await _context.Set<TBaseEntity>().SingleOrDefaultAsync(sl => sl.Id == Id);
        }

        public virtual void Add(TBaseEntity baseEntity)
        {
            _context.Add(baseEntity);
        }
        public void Update(TBaseEntity baseEntity)
        {
            _context.Set<TBaseEntity>().Update(baseEntity);
        }
        public virtual void Remove(TBaseEntity baseEntity)
        {
            _context.Remove(baseEntity);
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TBaseEntity> GetAllAsync(Expression<Func<TBaseEntity, bool>> filter)
        {
            return _context.Set<TBaseEntity>().Where(filter);
        }
    }
}
