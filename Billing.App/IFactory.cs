using System.Threading.Tasks;

namespace Billing.App
{
    public interface IFactory<in TArg, TResult>
    {
        Task<TResult> Create(TArg arg);
    }
}