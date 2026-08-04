using System;
using System.IO;
using System.Text;

namespace UCS.PacketProcessing
{
	// Token: 0x0200008F RID: 143
	public class MessageReader : BinaryReader
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000C56B File Offset: 0x0000A76B
		public MessageReader(Stream input)
			: base(input)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012645 File Offset: 0x00010845
		public override double ReadDouble()
		{
			return BitConverter.ToDouble(this.ReadByteArrayEndian(8), 0);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C682 File Offset: 0x0000A882
		public override long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012654 File Offset: 0x00010854
		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.ReadByteArrayEndian(8), 0);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012663 File Offset: 0x00010863
		public override float ReadSingle()
		{
			return BitConverter.ToSingle(this.ReadByteArrayEndian(4), 0);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C67A File Offset: 0x0000A87A
		public override int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012672 File Offset: 0x00010872
		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.ReadByteArrayEndian(4), 0);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C64A File Offset: 0x0000A84A
		public override short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012681 File Offset: 0x00010881
		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.ReadByteArrayEndian(2), 0);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00012690 File Offset: 0x00010890
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

		// Token: 0x060003CC RID: 972 RVA: 0x000126CC File Offset: 0x000108CC
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

		// Token: 0x060003CD RID: 973 RVA: 0x000126FC File Offset: 0x000108FC
		private byte[] ReadByteArrayEndian(int count)
		{
			byte[] array = this.ReadBytes(count);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012720 File Offset: 0x00010920
		private void CheckLength(int length, string typeName)
		{
			if (length < -1)
			{
				Console.WriteLine(string.Concat(new object[] { "The length of a ", typeName, " was invalid: ", length }));
			}
			if ((long)length > this.BaseStream.Length - this.BaseStream.Position)
			{
				Console.WriteLine(string.Concat(new object[] { "The length of a ", typeName, " was larger than the remaining bytes: ", length }));
			}
		}
	}
}
