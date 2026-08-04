namespace CRS.Extensions
{
    internal static class Crypto
    {
        /// <summary>
        /// Increment the specified nonce.
        /// </summary>
        /// <param name="_Nonce">The nonce.</param>
        public static void Increment(this byte[] _Nonce)
        {
            for (int j = 0; j < 2; j++)
            {
                ushort c = 1;
                for (uint i = 0; i < _Nonce.Length; i++)
                {
                    c += _Nonce[i];
                    _Nonce[i] = (byte)c;
                    c >>= 8;
                }
            }
        }
    }
}