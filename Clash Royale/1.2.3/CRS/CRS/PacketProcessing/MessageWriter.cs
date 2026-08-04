using System;
using System.IO;
using System.Text;

namespace UCS.PacketProcessing
{
	// Token: 0x02000091 RID: 145
	public class MessageWriter : BinaryWriter
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00012EDE File Offset: 0x000110DE
		public MessageWriter()
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012EE6 File Offset: 0x000110E6
		public MessageWriter(Stream output)
			: base(output)
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012EEF File Offset: 0x000110EF
		public override void Write(decimal value)
		{
			this.CheckDispose();
			this.Write((double)value);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012F04 File Offset: 0x00011104
		public override void Write(double value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00012F25 File Offset: 0x00011125
		public override void Write(long value)
		{
			this.CheckDispose();
			this.Write((ulong)value);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012F34 File Offset: 0x00011134
		public override void Write(ulong value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00012F58 File Offset: 0x00011158
		public override void Write(float value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00012F79 File Offset: 0x00011179
		public override void Write(int value)
		{
			this.CheckDispose();
			this.Write((uint)value);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012F88 File Offset: 0x00011188
		public override void Write(uint value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00012FA9 File Offset: 0x000111A9
		public override void Write(short value)
		{
			this.CheckDispose();
			this.Write((ushort)value);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012FBC File Offset: 0x000111BC
		public override void Write(ushort value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00012FE0 File Offset: 0x000111E0
		public override void Write(string value)
		{
			this.CheckDispose();
			if (value == null)
			{
				this.Write(-1);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this.Write(bytes.Length);
			this.Write(bytes);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001301A File Offset: 0x0001121A
		public override void Write(bool value)
		{
			this.CheckDispose();
			base.Write(value);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013029 File Offset: 0x00011229
		public void Write(byte[] buffer, bool prefixed)
		{
			this.CheckDispose();
			this.Write(buffer, 0, buffer.Length, prefixed);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001303D File Offset: 0x0001123D
		public void Write(byte[] buffer, int index, int count, bool prefixed)
		{
			this.CheckDispose();
			if (!prefixed)
			{
				this.Write(buffer, index, count);
				return;
			}
			this.Write(buffer.Length);
			this.Write(buffer, index, count);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013065 File Offset: 0x00011265
		public new void Dispose()
		{
			this.Dispose(true);
			this._disposed = true;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013075 File Offset: 0x00011275
		private void CheckDispose()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, "Cannot access the MessageWriter object because it was disposed.");
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001308B File Offset: 0x0001128B
		private void WriteByteArrayEndian(byte[] buffer)
		{
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(buffer);
			}
			this.Write(buffer);
		}

		// Token: 0x04000273 RID: 627
		private bool _disposed;
	}
}
