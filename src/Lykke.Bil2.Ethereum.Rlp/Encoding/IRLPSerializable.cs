namespace Lykke.Bil2.Ethereum.Rlp.Encoding
{
    public interface IRLPSerializable
    {
        RLPItem Serialize();
        void Deserialize(RLPItem item);
    }
}
