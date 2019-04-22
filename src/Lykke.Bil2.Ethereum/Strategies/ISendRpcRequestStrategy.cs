using System.Threading.Tasks;

namespace Lykke.Bil2.Ethereum.Strategies
{
    public interface ISendRpcRequestStrategy
    {
        Task<string> ExecuteAsync(
            string requestJson);
    }
}
