using Domain.Common;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        Task<List<TBaseEntity>> GetAll();
        IQueryable<TBaseEntity> GetAllAsync(Expression<Func<TBaseEntity, bool>> filter);
        Task<TBaseEntity> GetByIdAsync(int Id);
        void Add(TBaseEntity baseEntity);
        void Update(TBaseEntity baseEntity);
        void Remove(TBaseEntity baseEntity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
