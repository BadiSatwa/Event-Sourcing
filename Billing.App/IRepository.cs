using System.Threading.Tasks;
using Billing.Domain;

namespace Billing.App
{
    public interface IRepository<in TId, TAgg> 
        where TAgg : AggregateRoot<TId>
        where TId : IAggregateRootId
    {
        Task<TAgg> GetById(TId id);
        Task Save(TAgg aggregate);
    }
}