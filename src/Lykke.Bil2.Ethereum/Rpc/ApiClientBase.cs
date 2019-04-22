using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Bil2.Ethereum.Exceptions;
using Lykke.Bil2.Ethereum.Models;
using Lykke.Bil2.Ethereum.Strategies;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ethereum.Rpc
{
    public abstract class ApiClientBase
    {
        private readonly ISendRpcRequestStrategy _sendRpcRequestStrategy;

        protected ApiClientBase(
            ISendRpcRequestStrategy sendRpcRequestStrategy)
        {
            _sendRpcRequestStrategy = sendRpcRequestStrategy;
        }


        protected async Task<RpcResponse> SendRpcRequestAsync(
            RpcRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var responseJson = await _sendRpcRequestStrategy.ExecuteAsync(requestJson);
            var response = JsonConvert.DeserializeObject<RpcResponse>(responseJson);

            if (response.Error != null)
            {
                var error = response.Error;

                throw new RpcErrorException(requestJson, error.Code, error.Message);
            }
            else
            {
                return response;
            }
        }
    }
}
