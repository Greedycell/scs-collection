using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000039 RID: 57
	public class GenericHash
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(64);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D8F7 File Offset: 0x0000BAF7
		public static byte[] Hash(string message, string key, int bytes)
		{
			return GenericHash.Hash(message, Encoding.UTF8.GetBytes(key), bytes);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D90B File Offset: 0x0000BB0B
		public static byte[] Hash(string message, byte[] key, int bytes)
		{
			return GenericHash.Hash(Encoding.UTF8.GetBytes(message), key, bytes);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D920 File Offset: 0x0000BB20
		public static byte[] Hash(byte[] message, byte[] key, int bytes)
		{
			int num;
			if (key != null)
			{
				if (key.Length > 64 || key.Length < 16)
				{
					throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
				}
				num = key.Length;
			}
			else
			{
				key = new byte[0];
				num = 0;
			}
			if (bytes > 64 || bytes < 16)
			{
				throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
			}
			byte[] array = new byte[bytes];
			SodiumLibrary.crypto_generichash(array, array.Length, message, (long)message.Length, key, num);
			return array;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D9BD File Offset: 0x0000BBBD
		public static byte[] HashSaltPersonal(string message, string key, string salt, string personal, int bytes = 64)
		{
			return GenericHash.HashSaltPersonal(Encoding.UTF8.GetBytes(message), Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(salt), Encoding.UTF8.GetBytes(personal), bytes);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public static byte[] HashSaltPersonal(byte[] message, byte[] key, byte[] salt, byte[] personal, int bytes = 64)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message", "Message cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if (personal == null)
			{
				throw new ArgumentNullException("personal", "Personal string cannot be null");
			}
			if (key != null && (key.Length > 64 || key.Length < 16))
			{
				throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
			}
			if (key == null)
			{
				key = new byte[0];
			}
			if (salt.Length != 16)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 16));
			}
			if (personal.Length != 16)
			{
				throw new PersonalOutOfRangeException(string.Format("Personal bytes must be {0} bytes in length.", 16));
			}
			if (bytes > 64 || bytes < 16)
			{
				throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
			}
			byte[] array = new byte[bytes];
			SodiumLibrary.crypto_generichash_blake2b_salt_personal(array, array.Length, message, (long)message.Length, key, key.Length, salt, personal);
			return array;
		}

		// Token: 0x04000183 RID: 387
		private const int BYTES_MIN = 16;

		// Token: 0x04000184 RID: 388
		private const int BYTES_MAX = 64;

		// Token: 0x04000185 RID: 389
		private const int KEY_BYTES_MIN = 16;

		// Token: 0x04000186 RID: 390
		private const int KEY_BYTES_MAX = 64;

		// Token: 0x04000187 RID: 391
		private const int OUT_BYTES = 64;

		// Token: 0x04000188 RID: 392
		private const int SALT_BYTES = 16;

		// Token: 0x04000189 RID: 393
		private const int PERSONAL_BYTES = 16;

		// Token: 0x020000D6 RID: 214
		public class GenericHashAlgorithm : HashAlgorithm
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x00018642 File Offset: 0x00016842
			public GenericHashAlgorithm(string key, int bytes)
				: this(Encoding.UTF8.GetBytes(key), bytes)
			{
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x00018658 File Offset: 0x00016858
			public GenericHashAlgorithm(byte[] key, int bytes)
			{
				this.hashStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SodiumLibrary._HashState)));
				if (key != null)
				{
					if (key.Length > 64 || key.Length < 16)
					{
						throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
					}
					int num = key.Length;
				}
				else
				{
					key = new byte[0];
				}
				this.key = key;
				if (bytes > 64 || bytes < 16)
				{
					throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
				}
				this.bytes = bytes;
				this.Initialize();
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x0001870C File Offset: 0x0001690C
			~GenericHashAlgorithm()
			{
				Marshal.FreeHGlobal(this.hashStatePtr);
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x00018740 File Offset: 0x00016940
			public override void Initialize()
			{
				SodiumLibrary.hash_init(this.hashStatePtr, this.key, this.key.Length, this.bytes);
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00018768 File Offset: 0x00016968
			protected override void HashCore(byte[] array, int ibStart, int cbSize)
			{
				byte[] array2 = new byte[cbSize];
				Array.Copy(array, ibStart, array2, 0, cbSize);
				SodiumLibrary.hash_update(this.hashStatePtr, array2, (long)cbSize);
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0001879C File Offset: 0x0001699C
			protected override byte[] HashFinal()
			{
				byte[] array = new byte[this.bytes];
				SodiumLibrary.hash_final(this.hashStatePtr, array, this.bytes);
				return array;
			}

			// Token: 0x040003A3 RID: 931
			private readonly int bytes;

			// Token: 0x040003A4 RID: 932
			private readonly IntPtr hashStatePtr;

			// Token: 0x040003A5 RID: 933
			private readonly byte[] key;
		}
	}
}
