﻿using Lykke.Bil2.Ethereum.Models;
using Lykke.Bil2.Ethereum.Rpc;
using Lykke.Bil2.Ethereum.Strategies;
using Lykke.Bil2.Ethereum.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Lykke.Bil2.Ethereum
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultEthApiClient : ApiClientBase, IEthApiClient
    {
        private static string _erc20TransferTopic = "0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef";
        protected const string BestBlockIdentifier = "latest";
        protected const string PendingBlockIdentifier = "pending";

        public DefaultEthApiClient(
            ISendRpcRequestStrategy sendRpcRequestStrategy)

            : base(sendRpcRequestStrategy)
        {

        }

        /// <inheritdoc />
        public async Task<BigInteger> EstimateGasAmountAsync(
            string from,
            string to,
            BigInteger transferAmount)
        {
            var requestParams = new object[] { new { from, to, value = transferAmount.ToHexString() } };
            var request = new RpcRequest("eth_estimateGas", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        /// <inheritdoc />
        public Task<BigInteger> GetBalanceAsync(
            string address)
        {
            return GetBalanceAsync(address, BestBlockIdentifier);
        }

        /// <inheritdoc />
        public Task<BigInteger> GetBalanceAsync(
            string address,
            BigInteger blockNumber)
        {
            return GetBalanceAsync(address, blockNumber.ToHexString());
        }

        /// <inheritdoc />
        public async Task<BigInteger> GetBestBlockNumberAsync()
        {
            var request = new RpcRequest("eth_blockNumber");
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        /// <inheritdoc />
        public async Task<Block> GetBlockAsync(
            bool includeTransactions)
        {
            var requestParams = new object[] { BestBlockIdentifier, includeTransactions };
            var request = new RpcRequest("eth_getBlockByNumber", requestParams);
            var response = await SendRpcRequestAsync(request);

            return GetBlock(response, includeTransactions);
        }

        /// <inheritdoc />
        public async Task<Block> GetBlockAsync(
            string blockHash,
            bool includeTransactions)
        {
            var requestParams = new object[] { blockHash, includeTransactions };
            var request = new RpcRequest("eth_getBlockByHash", requestParams);
            var response = await SendRpcRequestAsync(request);

            return GetBlock(response, includeTransactions);
        }

        /// <inheritdoc />
        public async Task<Block> GetBlockAsync(
            BigInteger blockNumber,
            bool includeTransactions)
        {
            var requestParams = new object[] { $"{blockNumber.ToHexString()}", includeTransactions };
            var request = new RpcRequest("eth_getBlockByNumber", requestParams);
            var response = await SendRpcRequestAsync(request);

            return GetBlock(response, includeTransactions);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilterLog>> GetBlockLogsAsync(BigInteger blockNumber)
        {
            var blockNumberStr = $"{blockNumber.ToHexString()}";
            
            var requestParams = new object[] { blockNumberStr, blockNumberStr, new object[]
            {
                _erc20TransferTopic
            }};
            var request = new RpcRequest("eth_getLogs", requestParams);
            var response = await SendRpcRequestAsync(request);

            if (response.Result.Type != JTokenType.Null)
            {
                var logs = response.Result.Value<JArray>().ToList().Select(GetFilterLog);

                return logs;
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<string> GetCodeAsync(
            string address)
        {
            var requestParams = new object[] { address, BestBlockIdentifier };
            var request = new RpcRequest("eth_getCode", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<string>();
        }

        /// <inheritdoc />
        public async Task<BigInteger> GetGasPriceAsync()
        {
            var request = new RpcRequest("eth_gasPrice");
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        /// <inheritdoc />
        public async Task<Transaction> GetTransactionAsync(
            string transactionHash)
        {
            var requestParams = new object[] { transactionHash };
            var request = new RpcRequest("eth_getTransactionByHash", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.Result.Type != JTokenType.Null
                 ? GetTransaction(response.Result)
                 : null;
        }

        /// <inheritdoc />
        public async Task<BigInteger> GetTransactionCountAsync(
            string address,
            bool includePendingTransactions)
        {
            var blockIdentifier = includePendingTransactions ? PendingBlockIdentifier : BestBlockIdentifier;
            var requestParams = new object[] { address, blockIdentifier };
            var request = new RpcRequest("eth_getTransactionCount", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        /// <inheritdoc />
        public async Task<TransactionReceipt> GetTransactionReceiptAsync(
            string transactionHash)
        {
            var requestParams = new object[] { transactionHash };
            var request = new RpcRequest("eth_getTransactionReceipt", requestParams);
            var response = await SendRpcRequestAsync(request);

            if (response.Result.Type != JTokenType.Null)
            {
                return new TransactionReceipt
                (
                    blockHash: response.ResultValue<string>("blockHash"),
                    blockNumber: response.ResultValue<BigInteger?>("blockNumber"),
                    contractAddress: response.ResultValue<string>("contractAddress"),
                    cumulativeGasUsed: response.ResultValue<BigInteger>("cumulativeGasUsed"),
                    gasUsed: response.ResultValue<BigInteger>("gasUsed"),
                    status: response.ResultValue<BigInteger?>("status"),
                    transactionIndex: response.ResultValue<BigInteger>("transactionIndex"),
                    transactionHash: response.ResultValue<string>("transactionHash")
                );
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<string> SendRawTransactionAsync(
            string transactionData)
        {
            var requestParams = new object[] { transactionData };
            var request = new RpcRequest("eth_sendRawTransaction", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<string>();
        }

        public async Task<BigInteger> GetNextNonceAsync(
            string address)
        {
            var requestParams = new object[] { address };
            var request = new RpcRequest("parity_nextNonce", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        public async Task<IEnumerable<TransactionTrace>> GetTransactionTraces(
            string transactionHash)
        {
            var requestParams = new object[] { transactionHash };
            var request = new RpcRequest("trace_transaction", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.Result.Value<JArray>()
                .Select(GetTransactionTrace);
        }

        private static TransactionTrace GetTransactionTrace(
            JToken jToken)
        {
            var action = jToken.SelectToken("action");

            return new TransactionTrace
            (
                action: new TransactionTrace.TransactionAction
                (
                    callType: action.Value<string>("callType"),
                    from: action.Value<string>("from"),
                    to: action.Value<string>("to"),
                    value: action.Value<string>("value").HexToBigInteger()
                ),
                blockHash: jToken.Value<string>("blockHash"),
                blockNumber: jToken.Value<string>("blockNumber").HexToBigInteger(),
                error: jToken.Value<string>("error"),
                transactionHash: jToken.Value<string>("transactionHash"),
                type: jToken.Value<string>("type")
            );
        }

        private async Task<BigInteger> GetBalanceAsync(
            string address,
            string blockIdentifier)
        {
            var requestParams = new object[] { address, blockIdentifier };
            var request = new RpcRequest("eth_getBalance", requestParams);
            var response = await SendRpcRequestAsync(request);

            return response.ResultValue<BigInteger>();
        }

        private Block GetBlock(
            RpcResponse response,
            bool includeTransactions)
        {
            if (response.Result.Type != JTokenType.Null)
            {
                var transactions = response.Result.Value<JArray>("transactions").ToList();

                IEnumerable<Transaction> GetTransactions()
                {
                    return includeTransactions
                        ? transactions.Select(GetTransaction)
                        : null;
                }

                // ReSharper disable once ImplicitlyCapturedClosure
                IEnumerable<string> GetTransactionHashes()
                {
                    return includeTransactions
                        ? transactions.Select(x => x.Value<string>("hash"))
                        : transactions.Select(x => x.Value<string>());
                }

                return new Block
                (
                    blockHash: response.ResultValue<string>("hash"),
                    number: response.ResultValue<BigInteger?>("number"),
                    parentHash: response.ResultValue<string>("parentHash"),
                    timestamp: response.ResultValue<BigInteger>("timestamp"),
                    transactionHashes: GetTransactionHashes(),
                    transactions: GetTransactions()
                );
            }
            else
            {
                return null;
            }
        }

        private Transaction GetTransaction(
            JToken jToken)
        {
            return new Transaction
            (
                blockHash: jToken.Value<string>("blockHash"),
                blockNumber: jToken.Value<string>("blockNumber").HexToNullableBigInteger(),
                from: jToken.Value<string>("from"),
                gas: jToken.Value<string>("gas").HexToBigInteger(),
                gasPrice: jToken.Value<string>("gasPrice").HexToBigInteger(),
                input: jToken.Value<string>("input"),
                nonce: jToken.Value<string>("nonce").HexToBigInteger(),
                to: jToken.Value<string>("to"),
                transactionIndex: jToken.Value<string>("transactionIndex").HexToNullableBigInteger(),
                transactionHash: jToken.Value<string>("hash"),
                value: jToken.Value<string>("value").HexToBigInteger()
            );
        }

        private FilterLog GetFilterLog(
            JToken jToken)
        {
            return new FilterLog
            (
                type: jToken.Value<string>("blockHash"),
                logIndex : jToken.Value<string>("logIndex").HexToBigInteger(),
                transactionHash: jToken.Value<string>("transactionHash"),
                blockHash: jToken.Value<string>("blockHash"),
                blockNumber: jToken.Value<string>("blockNumber").HexToBigInteger(),
                transactionIndex: jToken.Value<string>("transactionIndex").HexToBigInteger(),
                address: jToken.Value<string>("address"),
                data: jToken.Value<string>("address"),
                removed: jToken.Value<bool>("removed"),
                topics: jToken.Value<object[]>("topics")
            );
        }
    }
}
