namespace CRS.Library.Sodium
{
    internal static class Sodium
    {
        /// <summary>
        /// Encrypt a packet using Sodium Public Box.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <param name="_PrivateKey">The private key.</param>
        /// <param name="_PublicKey">The public key.</param>
        /// <returns>The encrypted packet in bytes.</returns>
        public static byte[] Encrypt(byte[] _Packet, byte[] _Nonce, byte[] _PrivateKey, byte[] _PublicKey)
        {
            return new PublicBox(_PrivateKey, _PublicKey).Encrypt(_Packet, _Nonce);
        }

        /// <summary>
        /// Encrypt a packet using Sodium Secret Box.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <param name="_SharedKey">The shared key.</param>
        /// <returns>The encrypted packet in bytes.</returns>
        public static byte[] Encrypt(byte[] _Packet, byte[] _Nonce, byte[] _SharedKey)
        {
            return new SecretBox(_SharedKey).Encrypt(_Packet, _Nonce);
        }

        /// <summary>
        /// Generate a key pair.
        /// </summary>
        /// <param name="_PublicKey">The public key.</param>
        /// <param name="_PrivateKey">The private key.</param>
        /// <returns>The generated KeyPair.</returns>
        public static KeyPairGL GenerateKeyPair(byte[] _PublicKey, byte[] _PrivateKey)
        {
            return new KeyPairGL(_PublicKey, _PrivateKey);
        }

        /// <summary>
        /// Decrypt a packet using Sodium Public Box.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <param name="_PrivateKey">The private key.</param>
        /// <param name="_PublicKey">The public key.</param>
        /// <returns>The decrypted packet in bytes.</returns>
        public static byte[] Decrypt(byte[] _Packet, byte[] _Nonce, byte[] _PrivateKey, byte[] _PublicKey)
        {
            return new PublicBox(_PrivateKey, _PublicKey).Decrypt(_Packet, _Nonce);
        }

        /// <summary>
        /// Decrypt a packet using Sodium Secret Box.
        /// </summary>
        /// <param name="_Packet">The packet.</param>
        /// <param name="_Nonce">The nonce.</param>
        /// <param name="_SharedKey">The shared key.</param>
        /// <returns>The decrypted packet in bytes.</returns>
        public static byte[] Decrypt(byte[] _Packet, byte[] _Nonce, byte[] _SharedKey)
        {
            return new SecretBox(_SharedKey).Decrypt(_Packet, _Nonce);
        }
    }
}