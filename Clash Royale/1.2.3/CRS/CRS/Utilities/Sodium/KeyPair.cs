using System;
using System.Security.Cryptography;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200003A RID: 58
	public class KeyPair : IDisposable
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public KeyPair(byte[] publicKey, byte[] privateKey)
		{
			if (privateKey.Length % 16 != 0)
			{
				throw new KeyOutOfRangeException("Private Key length must be a multiple of 16 bytes.");
			}
			this.PublicKey = publicKey;
			this._privateKey = privateKey;
			this._ProtectKey();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000DB37 File Offset: 0x0000BD37
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		public byte[] PublicKey { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public byte[] PrivateKey
		{
			get
			{
				this._UnprotectKey();
				byte[] array = new byte[this._privateKey.Length];
				Array.Copy(this._privateKey, array, array.Length);
				this._ProtectKey();
				return array;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000DB7F File Offset: 0x0000BD7F
		public void Dispose()
		{
			if (this._privateKey != null && this._privateKey.Length != 0)
			{
				Array.Clear(this._privateKey, 0, this._privateKey.Length);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		~KeyPair()
		{
			this.Dispose();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		private void _ProtectKey()
		{
			if (!SodiumLibrary.IsRunningOnMono)
			{
				ProtectedMemory.Protect(this._privateKey, MemoryProtectionScope.SameProcess);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		private void _UnprotectKey()
		{
			if (!SodiumLibrary.IsRunningOnMono)
			{
				ProtectedMemory.Unprotect(this._privateKey, MemoryProtectionScope.SameProcess);
			}
		}

		// Token: 0x0400018A RID: 394
		private readonly byte[] _privateKey;
	}
}
