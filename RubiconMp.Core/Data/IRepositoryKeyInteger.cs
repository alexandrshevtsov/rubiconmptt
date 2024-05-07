using RubiconMp.Core.Domain;

namespace RubiconMp.Core.Data
{
    public interface IRepositoryKeyInteger<T> : IRepository<T, int> where T : BaseEntityWithIntegerKey
    {
    }
}
