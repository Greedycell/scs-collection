using System;
using System.IO;
using System.Text;

namespace CRS.PacketProcessing
{
	// Token: 0x0200006D RID: 109
	public class PacketReader : BinaryReader
	{
		// Token: 0x0600032B RID: 811 RVA: 0x000181AF File Offset: 0x000163AF
		public PacketReader(Stream input)
			: base(input)
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000181B8 File Offset: 0x000163B8
		public byte[] ReadBytes()
		{
			int num = this.ReadInt32();
			this.CheckLength(num, "byte array");
			if (num == -1)
			{
				return null;
			}
			return this.ReadBytes(num);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000181E5 File Offset: 0x000163E5
		public override double ReadDouble()
		{
			return BitConverter.ToDouble(this.ReadByteArrayEndian(8), 0);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000181F4 File Offset: 0x000163F4
		public override short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000181FD File Offset: 0x000163FD
		public override int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00018205 File Offset: 0x00016405
		public override long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001820D File Offset: 0x0001640D
		public override float ReadSingle()
		{
			return BitConverter.ToSingle(this.ReadByteArrayEndian(4), 0);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001821C File Offset: 0x0001641C
		public override string ReadString()
		{
			int num = this.ReadInt32();
			this.CheckLength(num, "string");
			if (num == -1)
			{
				return null;
			}
			byte[] array = this.ReadBytes(num);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00018255 File Offset: 0x00016455
		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.ReadByteArrayEndian(2), 0);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00018264 File Offset: 0x00016464
		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.ReadByteArrayEndian(4), 0);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00018273 File Offset: 0x00016473
		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.ReadByteArrayEndian(8), 0);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000123B6 File Offset: 0x000105B6
		private void CheckLength(int length, string typeName)
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00018284 File Offset: 0x00016484
		private byte[] ReadByteArrayEndian(int count)
		{
			byte[] array = this.ReadBytes(count);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}
	}
}
