using System;

namespace Lykke.Bil2.Ethereum.Exceptions
{
    public class RpcException : Exception
    {
        public RpcException(
            string message,
            string request,
            Exception inner = null)
        
            : base(message, inner)
        {
            Request = request;
        }
        
        public string Request { get; }
    }
}
