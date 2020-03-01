using System.Threading.Tasks;

namespace Billing.App
{
    public interface IQueryModelManager
    {
        Task RecreateModel();
    }
}