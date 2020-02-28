using System.Threading.Tasks;

namespace Billing.App
{
    public interface IViewModelQuery<in TArg, TResult>
    {
        Task<TResult> Execute(TArg arg);
    }
}