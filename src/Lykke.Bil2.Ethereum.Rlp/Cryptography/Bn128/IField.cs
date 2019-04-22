using System.Numerics;

namespace Lykke.Bil2.Ethereum.Rlp.Cryptography.Bn128
{
    public interface IField<T>
    {
        T Zero { get; }
        T One { get; }
        T Add(T other);
        T Add(BigInteger other);
        T Subtract(T other);
        T Subtract(BigInteger other);
        T Multiply(T other);
        T Multiply(BigInteger other);
        T Divide(T other);
        T Divide(BigInteger other);
        T Negate();
        T Inverse();
        T Pow(BigInteger exponent);
    }
}
