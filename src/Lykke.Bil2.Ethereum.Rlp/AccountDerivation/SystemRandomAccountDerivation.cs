using System;
using System.Security.Cryptography;

namespace Lykke.Bil2.Ethereum.Rlp.AccountDerivation
{
    public class SystemRandomAccountDerivation : AccountDerivationBase
    {
        Action<byte[]> _getRandomBytes;

        public SystemRandomAccountDerivation(int? seed = null)
        {
            if (seed.HasValue)
            {
                var rnd = new Random(seed.Value);
                _getRandomBytes = bytes =>
                {
                    rnd.NextBytes(bytes);
                };
            }
            else
            {
                var rnd = RandomNumberGenerator.Create();
                _getRandomBytes = bytes =>
                {
                    rnd.GetBytes(bytes);
                };
            }
        }

        public override byte[] GeneratePrivateKey(uint accountIndex)
        {
            var data = new byte[32];
            _getRandomBytes(data);
            return data;
        }
    }
}
