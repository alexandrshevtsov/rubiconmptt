using Microsoft.EntityFrameworkCore;
using RubiconMp.Core.Data;
using RubiconMp.Core.Domain;

namespace RubiconMp.Data
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> Items => Entities;

        public async Task Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(await GetFullErrorText(dbEx), dbEx);
            }
        }

        public async Task<T> GetById(TKey id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await Entities.AddAsync(entity);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(await GetFullErrorText(dbEx), dbEx);
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Update(entity);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(await GetFullErrorText(dbEx), dbEx);
            }
        }

        protected virtual DbSet<T> Entities
        {
            get => _context.Set<T>();
        }

        protected async Task<string> GetFullErrorText(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            await _context.SaveChangesAsync();
            return exception.ToString();
        }
    }
}
