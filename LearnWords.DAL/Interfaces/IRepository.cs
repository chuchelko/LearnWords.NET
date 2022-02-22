using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWords.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : UniqueWithId
{
    Task AddAsync(TEntity entity);
    TEntity? Get(Guid id);
    Task UpdateAsync(TEntity entity);
    void Update(TEntity entity);
    Task DeleteAsync(Guid id);
    Task SaveAsync();
}
