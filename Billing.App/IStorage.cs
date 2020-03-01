using System.Threading.Tasks;

namespace Billing.App
{
    public interface IStorage
    {
        Task ClearStorage();
    }
}