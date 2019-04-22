using JetBrains.Annotations;
using System.ComponentModel;
using System.Numerics;

namespace Lykke.Bil2.Ethereum.Models
{
    [PublicAPI, ImmutableObject(true)]
    public class FilterLog
    {
        public FilterLog(string type,
            BigInteger logIndex,
            string transactionHash,
            string blockHash,
            BigInteger blockNumber,
            BigInteger transactionIndex,
            string address,
            string data,
            bool removed,
            object[] topics)
        {
            Removed = removed;
            Type = type;
            LogIndex = logIndex;
            TransactionHash = transactionHash;
            TransactionIndex = transactionIndex;
            BlockHash = blockHash;
            BlockNumber = blockNumber;
            Address = address;
            Data = data;
            Topics = topics;
        }

        //[JsonProperty(PropertyName = "removed")]
        public bool Removed { get; }
        //[JsonProperty(PropertyName = "type")]
        public string Type { get; }
        //[JsonProperty(PropertyName = "logIndex")]
        public BigInteger LogIndex { get; }
        //[JsonProperty(PropertyName = "transactionHash")]
        public string TransactionHash { get; }
        //[JsonProperty(PropertyName = "transactionIndex")]
        public BigInteger TransactionIndex { get; }
        //[JsonProperty(PropertyName = "blockHash")]
        public string BlockHash { get; }
        //[JsonProperty(PropertyName = "blockNumber")]
        public BigInteger BlockNumber { get; }
        //[JsonProperty(PropertyName = "address")]
        public string Address { get; }
        //[JsonProperty(PropertyName = "data")]
        public string Data { get; }
        //[JsonProperty(PropertyName = "topics")]
        public object[] Topics { get; }
    }
}
