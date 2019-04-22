using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Lykke.Bil2.Ethereum.Models;
using Lykke.Bil2.Ethereum.Strategies;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ethereum.Tests
{
    public class SendRpcRequestStrategy : ISendRpcRequestStrategy
    {
        private readonly RpcRequest _expectedRequest;
        private readonly string _responseJson;

        public SendRpcRequestStrategy(
            string requestAndResponseSubPath,
            bool validateRequest)
        {
            _responseJson = File.ReadAllText($"./Responses/{requestAndResponseSubPath}");

            if (validateRequest)
            {
                _expectedRequest = JsonConvert.DeserializeObject<RpcRequest>
                (
                    File.ReadAllText($"./Requests/{requestAndResponseSubPath}")
                );
            }
        }
        
        public SendRpcRequestStrategy(
            string requestSubPath,
            string responseSubPath)
        {
            _responseJson = File.ReadAllText($"./Responses/{responseSubPath}");

            _expectedRequest = JsonConvert.DeserializeObject<RpcRequest>
            (
                File.ReadAllText($"./Requests/{requestSubPath}")
            );
        }
        
        
        public Task<string> ExecuteAsync(
                string requestJson)
        {
            if (_expectedRequest != null)
            {
                var actualRequest = JsonConvert.DeserializeObject<RpcRequest>(requestJson);

                actualRequest
                    .Id
                    .Should()
                    .NotBeNullOrEmpty();
                
                actualRequest
                    .JsonRpcVersion
                    .Should()
                    .Be(_expectedRequest.JsonRpcVersion);
                
                actualRequest
                    .Method
                    .Should()
                    .Be(_expectedRequest.Method);

                actualRequest
                    .Parameters
                    .Should()
                    .BeEquivalentTo(_expectedRequest.Parameters);
            }

            return Task.FromResult(_responseJson);
        }
    }
}
