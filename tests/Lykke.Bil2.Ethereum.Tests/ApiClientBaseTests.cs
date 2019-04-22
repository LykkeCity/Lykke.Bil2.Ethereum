using System.Threading.Tasks;
using FluentAssertions;
using Lykke.Bil2.Ethereum.Exceptions;
using Lykke.Bil2.Ethereum.Models;
using Lykke.Bil2.Ethereum.Rpc;
using Lykke.Bil2.Ethereum.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lykke.Bil2.Ethereum.Tests
{
    [TestClass]
    public class ApiClientBaseTests
    {
        [TestMethod]
        public async Task SendRpcRequestAsync__Error_Returned__Exception_Thrown()
        {
            var request = new RpcRequest("test");
            var strategyMock = new SendRpcRequestStrategy("error.json", false);
            var clientMock = new ApiClient(strategyMock);

            RpcErrorException exception = null;
            
            try
            {
                await clientMock.SendRpcRequestAsync(request);
            }
            catch (RpcErrorException e)
            {
                exception = e;
            }

            exception
                .Should()
                .NotBeNull();

            if (exception != null)
            {
                exception
                    .ErrorMessage
                    .Should()
                    .Be("Execution error");

                exception
                    .ErrorCode
                    .Should()
                    .Be(3);

                exception
                    .Request
                    .Should()
                    .NotBeNullOrEmpty();
            }

        }
        
        private class ApiClient : ApiClientBase
        {
            public ApiClient(
                ISendRpcRequestStrategy sendRpcRequestStrategy) 
                
                : base(sendRpcRequestStrategy)
            {
                
            }

            public new Task<RpcResponse> SendRpcRequestAsync(
                RpcRequest request)
            {
                return base.SendRpcRequestAsync(request);
            }
        }
    }
}
