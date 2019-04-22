using System.Numerics;
using Lykke.Bil2.Ethereum.Utils;

public static class BouncyCastleExtensions
{
    #region Functions
    public static Org.BouncyCastle.Math.BigInteger ToBouncyCastleBigInteger(this BigInteger bigInteger)
    {
        return new Org.BouncyCastle.Math.BigInteger(1, BigIntegerConverter.GetBytes(bigInteger));
    }

    public static BigInteger ToNumericsBigInteger(this Org.BouncyCastle.Math.BigInteger bcBigInteger)
    {
        return BigIntegerConverter.GetBigInteger(bcBigInteger.ToByteArrayUnsigned());
    }
    #endregion
}
