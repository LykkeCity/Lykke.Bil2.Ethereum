using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Bil2.Ethereum.Utils
{
    public abstract class GasDefinitions
    {
        #region Constants
        // Transaction Gas Definitions
        /// <summary>
        /// The amount of gas a transaction uses just to be processed.
        /// </summary>
        public const int GAS_TRANSACTION = 21000;
        /// <summary>
        /// The amount of gas a transaction uses when it has a zero byte in it's transaction data.
        /// </summary>
        public const int GAS_TRANSACTION_DATA_ZERO = 4;
        /// <summary>
        /// The amount of gas a transaction uses when it has a non-zero byte in it's transaction data.
        /// </summary>
        public const int GAS_TRANSACTION_DATA_NON_ZERO = 68;

        // EVM Gas Definitions

        // Memory
        /// <summary>
        /// The amount of gas used to allocate a word of memory.
        /// </summary>
        public const int GAS_MEMORY_BASE = 3;
        /// <summary>
        /// The amount of gas used to copy a word into memory.
        /// </summary>
        public const int GAS_MEMORY_COPY = 3; // cost per WORD copy

        // Arithmetic
        /// <summary>
        /// The amount of gas used for every byte of the exponent when executing an exponent instruction (pre-spurious dragon).
        /// </summary>
        public const int GAS_EXP_BYTE = 10;
        /// <summary>
        /// The amount of gas used for every byte of the exponent when executing an exponent instruction (post-spurious dragon).
        /// </summary>
        public const int GAS_EXP_BYTE_SPURIOUS_DRAGON = 50;

        // Cryptography
        /// <summary>
        /// The amount of gas used to keccak hash a word of data.
        /// </summary>
        public const int GAS_SHA3_WORD = 6;
        /// <summary>
        /// The amount of gas to execute the ECRecover Precompile.
        /// </summary>
        public const int GAS_PRECOMPILE_ECRECOVER = 3000;
        /// <summary>
        /// The amount of gas used just to execute a SHA256 precompile.
        /// </summary>
        public const int GAS_PRECOMPILE_SHA256_BASE = 60;
        /// <summary>
        /// The amount of gas used to SHA256 hash a word of data.
        /// </summary>
        public const int GAS_PRECOMPILE_SHA256_WORD = 12;
        /// <summary>
        /// The amount of gas used just to execute a RIPEMD160 precompile.
        /// </summary>
        public const int GAS_PRECOMPILE_RIPEMD160_BASE = 600;
        /// <summary>
        /// The amount of gas used to RIPEMD160 hash a word of data.
        /// </summary>
        public const int GAS_PRECOMPILE_RIPEMD160_WORD = 120;

        // Identity Precompile
        /// <summary>
        /// The amount of gas used just to execute an identity/memcpy precompile.
        /// </summary>
        public const int GAS_PRECOMPILE_IDENTITY_BASE = 15;
        /// <summary>
        /// The amount of gas used to identity/memcpy a word of data.
        /// </summary>
        public const int GAS_PRECOMPILE_IDENTITY_WORD = 3;

        // ModExp Precompile
        public const int GAS_PRECOMPILE_MODEXP_QUAD_DIVISOR = 20;

        // EC Precompiles
        /// <summary>
        /// The amount of gas used just to execute an EC add operation.
        /// </summary>
        public const int GAS_PRECOMPILE_ECADD_BASE = 500;
        /// <summary>
        /// The amount of gas used just to execute an EC multiplication operation.
        /// </summary>
        public const int GAS_PRECOMPILE_ECMUL_BASE = 40000;
        /// <summary>
        /// The amount of gas used just to execute an EC pairing operation.
        /// </summary>
        public const int GAS_PRECOMPILE_ECPAIRING_BASE = 100000;
        /// <summary>
        /// The amount of gas used per point supplied to the EC pairing operation.
        /// </summary>
        public const int GAS_PRECOMPILE_ECPAIRING_PER_POINT = 80000;

        // Storage
        /// <summary>
        /// The amount of gas used to add a key-value to account storage.
        /// </summary>
        public const int GAS_SSTORE_ADD = 20000;
        /// <summary>
        /// The amount of gas used to modify an existing value in account storage.
        /// </summary>
        public const int GAS_SSTORE_MODIFY = 5000;
        /// <summary>
        /// The amount of gas used to delete an existing value in account storage.
        /// </summary>
        public const int GAS_SSTORE_DELETE = 5000;
        /// <summary>
        /// The amount of gas refunded for deleting an existing key-value from account storage.
        /// </summary>
        public const int GAS_SSTORE_REFUND = 15000;

        // Logging
        /// <summary>
        /// The amount of gas used for every byte logged.
        /// </summary>
        public const int GAS_LOG_BYTE = 8;

        // Calls
        /// <summary>
        /// The amount of gas charged for calling an account that doesn't exist.
        /// </summary>
        public const int GAS_CALL_NEW_ACCOUNT = 25000;
        /// <summary>
        /// The amount of gas used to transfer value in a transaction.
        /// </summary>
        public const int GAS_CALL_VALUE = 9000;
        /// <summary>
        /// The amount of gas an inner call is given when transfering value in a transaction.
        /// </summary>
        public const int GAS_CALL_VALUE_STIPEND = 2300;
        /// <summary>
        /// The amount of gas that is refunded when an account self destructs.
        /// </summary>
        public const int GAS_SELF_DESTRUCT_REFUND = 24000;

        // Contract Creation
        /// <summary>
        /// The amount of gas used for every byte of a contract when creating a contract.
        /// </summary>
        public const int GAS_CONTRACT_BYTE = 200;
        #endregion
    }
}
