using System.Numerics;
using Lykke.Bil2.Ethereum.Models;
using Newtonsoft.Json.Linq;

namespace Lykke.Bil2.Ethereum.Utils
{
    public static class RpcResponseExtensions
    {
        public static T ResultValue<T>(
            this RpcResponse rpcResponse)
            => ResultValue<T>(rpcResponse.Result);

        public static T ResultValue<T>(
            this RpcResponse rpcResponse,
            string key)
            => ResultValue<T>(rpcResponse.Result[key]);

        private static T ResultValue<T>(
            JToken result)
        {
            if (typeof(T) == typeof(BigInteger))
            {
                return (T) (object) result.Value<string>().HexToBigInteger();
            }
            else if (typeof(T) == typeof(BigInteger?))
            {
                return (T) (object) result.Value<string>().HexToNullableBigInteger();
            }
            else
            {
                return result.Value<T>();
            }
        }
    }
}
