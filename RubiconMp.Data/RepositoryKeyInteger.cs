using RubiconMp.Core.Data;
using RubiconMp.Core.Domain;

namespace RubiconMp.Data
{
    public class RepositoryKeyInteger<T> : Repository<T, int>, IRepositoryKeyInteger<T> where T : BaseEntityWithIntegerKey
    {
        public RepositoryKeyInteger(ApplicationContext context) : base(context) { }
    }
}
