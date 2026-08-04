using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200003D RID: 61
	public class PasswordHash
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000DD61 File Offset: 0x0000BF61
		[Obsolete("Use ScryptGenerateSalt() or ArgonGenerateSalt() instead.")]
		public static byte[] GenerateSalt(PasswordHash.HashType hashType = PasswordHash.HashType.Scrypt)
		{
			if (hashType != PasswordHash.HashType.Argon)
			{
				return PasswordHash.ScryptGenerateSalt();
			}
			return PasswordHash.ArgonGenerateSalt();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] ScryptGenerateSalt()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000DD71 File Offset: 0x0000BF71
		public static byte[] ArgonGenerateSalt()
		{
			return SodiumCore.GetRandomBytes(16);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static byte[] ArgonHashBinary(byte[] password, byte[] salt, long opsLimit, int memLimit, long outputLength = 16L)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if ((long)salt.Length != 16L)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 16U));
			}
			if (opsLimit < 3L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit the number of passes, has to be at least 3");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			if (outputLength <= 0L)
			{
				throw new ArgumentOutOfRangeException("outputLength", "OutputLength cannot be zero or negative");
			}
			byte[] array = new byte[outputLength];
			if (SodiumLibrary.crypto_pwhash(array, (long)array.Length, password, (long)password.Length, salt, opsLimit, memLimit, 1) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return array;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public static byte[] ArgonHashBinary(string password, string salt, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive, long outputLength = 16L)
		{
			return PasswordHash.ArgonHashBinary(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), limit, outputLength);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public static byte[] ArgonHashBinary(byte[] password, byte[] salt, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive, long outputLength = 16L)
		{
			long num;
			int num2;
			switch (limit)
			{
			case PasswordHash.StrengthArgon.Interactive:
				num = 4L;
				num2 = 33554432;
				break;
			case PasswordHash.StrengthArgon.Moderate:
				num = 6L;
				num2 = 134217728;
				break;
			case PasswordHash.StrengthArgon.Sensitive:
				num = 8L;
				num2 = 536870912;
				break;
			default:
				num = 4L;
				num2 = 33554432;
				break;
			}
			return PasswordHash.ArgonHashBinary(password, salt, num, num2, outputLength);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		public static byte[] ArgonHashBinary(string password, string salt, long opsLimit, int memLimit, long outputLength = 16L)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] bytes2 = Encoding.UTF8.GetBytes(salt);
			return PasswordHash.ArgonHashBinary(bytes, bytes2, opsLimit, memLimit, outputLength);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public static string ArgonHashString(string password, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive)
		{
			long num;
			int num2;
			switch (limit)
			{
			case PasswordHash.StrengthArgon.Interactive:
				num = 4L;
				num2 = 33554432;
				break;
			case PasswordHash.StrengthArgon.Moderate:
				num = 6L;
				num2 = 134217728;
				break;
			case PasswordHash.StrengthArgon.Sensitive:
				num = 8L;
				num2 = 536870912;
				break;
			default:
				num = 4L;
				num2 = 33554432;
				break;
			}
			return PasswordHash.ArgonHashString(password, num, num2);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DF38 File Offset: 0x0000C138
		public static string ArgonHashString(string password, long opsLimit, int memLimit)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (opsLimit < 3L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit the number of passes, has to be at least 3");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			byte[] array = new byte[128];
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			if (SodiumLibrary.crypto_pwhash_str(array, bytes, (long)bytes.Length, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DFC1 File Offset: 0x0000C1C1
		public static bool ArgonHashStringVerify(string hash, string password)
		{
			return PasswordHash.ArgonHashStringVerify(Encoding.UTF8.GetBytes(hash), Encoding.UTF8.GetBytes(password));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000DFDE File Offset: 0x0000C1DE
		public static bool ArgonHashStringVerify(byte[] hash, byte[] password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (hash == null)
			{
				throw new ArgumentNullException("hash", "Hash cannot be null");
			}
			return SodiumLibrary.crypto_pwhash_str_verify(hash, password, (long)password.Length) == 0;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E018 File Offset: 0x0000C218
		public static string ScryptHashString(string password, PasswordHash.Strength limit = PasswordHash.Strength.Interactive)
		{
			long num;
			int num2;
			switch (limit)
			{
			case PasswordHash.Strength.Interactive:
				num = 524288L;
				num2 = 16777216;
				break;
			case PasswordHash.Strength.Moderate:
				num = 8388608L;
				num2 = 100000000;
				break;
			case PasswordHash.Strength.Medium:
				num = 8388608L;
				num2 = 134217728;
				break;
			case PasswordHash.Strength.MediumSlow:
				num = 33554432L;
				num2 = 134217728;
				break;
			case PasswordHash.Strength.Sensitive:
				num = 33554432L;
				num2 = 1073741824;
				break;
			default:
				num = 524288L;
				num2 = 16777216;
				break;
			}
			return PasswordHash.ScryptHashString(password, num, num2);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public static string ScryptHashString(string password, long opsLimit, int memLimit)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (opsLimit <= 0L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit cannot be zero or negative");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			byte[] array = new byte[102];
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			if (SodiumLibrary.crypto_pwhash_scryptsalsa208sha256_str(array, bytes, (long)bytes.Length, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E12A File Offset: 0x0000C32A
		public static byte[] ScryptHashBinary(string password, string salt, PasswordHash.Strength limit = PasswordHash.Strength.Interactive, long outputLength = 32L)
		{
			return PasswordHash.ScryptHashBinary(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), limit, outputLength);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E14C File Offset: 0x0000C34C
		public static byte[] ScryptHashBinary(byte[] password, byte[] salt, PasswordHash.Strength limit = PasswordHash.Strength.Interactive, long outputLength = 32L)
		{
			long num;
			int num2;
			switch (limit)
			{
			case PasswordHash.Strength.Interactive:
				num = 524288L;
				num2 = 16777216;
				break;
			case PasswordHash.Strength.Moderate:
				num = 8388608L;
				num2 = 100000000;
				break;
			case PasswordHash.Strength.Medium:
				num = 8388608L;
				num2 = 134217728;
				break;
			case PasswordHash.Strength.MediumSlow:
				num = 33554432L;
				num2 = 134217728;
				break;
			case PasswordHash.Strength.Sensitive:
				num = 33554432L;
				num2 = 1073741824;
				break;
			default:
				num = 524288L;
				num2 = 16777216;
				break;
			}
			return PasswordHash.ScryptHashBinary(password, salt, num, num2, outputLength);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public static byte[] ScryptHashBinary(string password, string salt, long opsLimit, int memLimit, long outputLength = 32L)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] bytes2 = Encoding.UTF8.GetBytes(salt);
			return PasswordHash.ScryptHashBinary(bytes, bytes2, opsLimit, memLimit, outputLength);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E208 File Offset: 0x0000C408
		public static byte[] ScryptHashBinary(byte[] password, byte[] salt, long opsLimit, int memLimit, long outputLength = 32L)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if ((long)salt.Length != 32L)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 32U));
			}
			if (opsLimit <= 0L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit cannot be zero or negative");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			if (outputLength <= 0L)
			{
				throw new ArgumentOutOfRangeException("outputLength", "OutputLength cannot be zero or negative");
			}
			byte[] array = new byte[outputLength];
			if (SodiumLibrary.crypto_pwhash_scryptsalsa208sha256(array, (long)array.Length, password, (long)password.Length, salt, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return array;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		public static bool ScryptHashStringVerify(string hash, string password)
		{
			return PasswordHash.ScryptHashStringVerify(Encoding.UTF8.GetBytes(hash), Encoding.UTF8.GetBytes(password));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public static bool ScryptHashStringVerify(byte[] hash, byte[] password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (hash == null)
			{
				throw new ArgumentNullException("hash", "Hash cannot be null");
			}
			return SodiumLibrary.crypto_pwhash_scryptsalsa208sha256_str_verify(hash, password, (long)password.Length) == 0;
		}

		// Token: 0x04000192 RID: 402
		private const int ARGON_ALGORITHM_DEFAULT = 1;

		// Token: 0x04000193 RID: 403
		private const uint ARGON_STRBYTES = 128U;

		// Token: 0x04000194 RID: 404
		private const uint ARGON_SALTBYTES = 16U;

		// Token: 0x04000195 RID: 405
		private const long ARGON_OPSLIMIT_INTERACTIVE = 4L;

		// Token: 0x04000196 RID: 406
		private const long ARGON_OPSLIMIT_MODERATE = 6L;

		// Token: 0x04000197 RID: 407
		private const long ARGON_OPSLIMIT_SENSITIVE = 8L;

		// Token: 0x04000198 RID: 408
		private const int ARGON_MEMLIMIT_INTERACTIVE = 33554432;

		// Token: 0x04000199 RID: 409
		private const int ARGON_MEMLIMIT_MODERATE = 134217728;

		// Token: 0x0400019A RID: 410
		private const int ARGON_MEMLIMIT_SENSITIVE = 536870912;

		// Token: 0x0400019B RID: 411
		private const uint SCRYPT_SALSA208_SHA256_STRBYTES = 102U;

		// Token: 0x0400019C RID: 412
		private const uint SCRYPT_SALSA208_SHA256_SALTBYTES = 32U;

		// Token: 0x0400019D RID: 413
		private const long SCRYPT_OPSLIMIT_INTERACTIVE = 524288L;

		// Token: 0x0400019E RID: 414
		private const long SCRYPT_OPSLIMIT_MODERATE = 8388608L;

		// Token: 0x0400019F RID: 415
		private const long SCRYPT_OPSLIMIT_MEDIUM = 8388608L;

		// Token: 0x040001A0 RID: 416
		private const long SCRYPT_OPSLIMIT_SENSITIVE = 33554432L;

		// Token: 0x040001A1 RID: 417
		private const int SCRYPT_MEMLIMIT_INTERACTIVE = 16777216;

		// Token: 0x040001A2 RID: 418
		private const int SCRYPT_MEMLIMIT_MODERATE = 100000000;

		// Token: 0x040001A3 RID: 419
		private const int SCRYPT_MEMLIMIT_MEDIUM = 134217728;

		// Token: 0x040001A4 RID: 420
		private const int SCRYPT_MEMLIMIT_SENSITIVE = 1073741824;

		// Token: 0x020000D7 RID: 215
		public enum HashType
		{
			// Token: 0x040003A7 RID: 935
			Argon,
			// Token: 0x040003A8 RID: 936
			Scrypt
		}

		// Token: 0x020000D8 RID: 216
		public enum Strength
		{
			// Token: 0x040003AA RID: 938
			Interactive,
			// Token: 0x040003AB RID: 939
			[Obsolete("Use Strength.Medium instead.")]
			Moderate,
			// Token: 0x040003AC RID: 940
			Medium,
			// Token: 0x040003AD RID: 941
			MediumSlow,
			// Token: 0x040003AE RID: 942
			Sensitive
		}

		// Token: 0x020000D9 RID: 217
		public enum StrengthArgon
		{
			// Token: 0x040003B0 RID: 944
			Interactive,
			// Token: 0x040003B1 RID: 945
			Moderate,
			// Token: 0x040003B2 RID: 946
			Sensitive
		}
	}
}
