using Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Application.Common.Interfaces
{
    public interface IRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        Task<List<TBaseEntity>> GetAll();
        Task<TBaseEntity> GetByIdAsync(int Id);
        void Add(TBaseEntity baseEntity);
        void Update(TBaseEntity baseEntity);
        void Remove(TBaseEntity baseEntity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
