using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Bil2.Ethereum.Rlp.DataTypes
{
    public enum EthereumChainId : uint
    {
        Ethereum_MainNet = 1,
        Morden = 2,
        Ropsten = 3,
        Rinkeby = 4,
        Rootstock_MainNet = 30,
        Rootstock_TestNet = 31,
        Kovan = 42,
        Ethereum_Classic_MainNet = 61,
        Ethereum_Classic_TestNet = 62,
        Geth_Private_Chains = 1337
    }
}
