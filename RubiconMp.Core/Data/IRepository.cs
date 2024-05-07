using RubiconMp.Core.Domain;

namespace RubiconMp.Core.Data
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<T> GetById(TKey id);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        IQueryable<T> Items { get; }
    }
}
