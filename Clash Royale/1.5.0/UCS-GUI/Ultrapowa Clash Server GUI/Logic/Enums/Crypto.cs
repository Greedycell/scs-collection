namespace CRS.Logic.Enums
{
    /// <summary>
    /// <see cref="Crypto"/> is an enum containing all cryptos for the server.
    /// </summary>
    internal enum Crypto : int
    {
        DEFAULT = 0,
        RC4_CRYPTO = 1,
        SODIUM_CRYPTO = 2,
        NONE = 3
    }
}