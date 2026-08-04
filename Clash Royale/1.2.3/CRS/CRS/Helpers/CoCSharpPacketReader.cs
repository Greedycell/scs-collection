using System;
using System.IO;
using System.Text;

namespace UCS.Helpers
{
	// Token: 0x02000027 RID: 39
	public class CoCSharpPacketReader : BinaryReader
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000C56B File Offset: 0x0000A76B
		public CoCSharpPacketReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C574 File Offset: 0x0000A774
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.BaseStream.Read(buffer, 0, count);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C584 File Offset: 0x0000A784
		public override bool ReadBoolean()
		{
			byte b = this.ReadByte();
			if (b == 0)
			{
				return false;
			}
			if (b == 1)
			{
				return true;
			}
			throw new Exception("Invalid.");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C5AD File Offset: 0x0000A7AD
		public override byte ReadByte()
		{
			return (byte)this.BaseStream.ReadByte();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		public byte[] ReadByteArray()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			if (num < -1)
			{
				throw new Exception("A byte array length was incorrect: " + num + ".");
			}
			if ((long)num > this.BaseStream.Length - this.BaseStream.Position)
			{
				throw new Exception(string.Format("A byte array was larger than remaining bytes. {0} > {1}.", num, this.BaseStream.Length - this.BaseStream.Position));
			}
			return this.ReadBytesWithEndian(num, false);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C64A File Offset: 0x0000A84A
		public override short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C654 File Offset: 0x0000A854
		public int ReadInt24()
		{
			byte[] array = this.ReadBytesWithEndian(3, false);
			return ((int)array[0] << 16) | ((int)array[1] << 8) | (int)array[2];
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C67A File Offset: 0x0000A87A
		public override int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C682 File Offset: 0x0000A882
		public override long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C68C File Offset: 0x0000A88C
		public override string ReadString()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			if (num < -1)
			{
				throw new Exception("A string length was incorrect: " + num);
			}
			if ((long)num > this.BaseStream.Length - this.BaseStream.Position)
			{
				throw new Exception(string.Format("A string was larger than remaining bytes. {0} > {1}.", num, this.BaseStream.Length - this.BaseStream.Position));
			}
			byte[] array = this.ReadBytesWithEndian(num, false);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C721 File Offset: 0x0000A921
		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.ReadBytesWithEndian(2, true), 0);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C731 File Offset: 0x0000A931
		public uint ReadUInt24()
		{
			return (uint)this.ReadInt24();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C739 File Offset: 0x0000A939
		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.ReadBytesWithEndian(4, true), 0);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C749 File Offset: 0x0000A949
		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.ReadBytesWithEndian(8, true), 0);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C759 File Offset: 0x0000A959
		public long Seek(long offset, SeekOrigin origin)
		{
			return this.BaseStream.Seek(offset, origin);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C768 File Offset: 0x0000A968
		private byte[] ReadBytesWithEndian(int count, bool switchEndian = true)
		{
			byte[] array = new byte[count];
			this.BaseStream.Read(array, 0, count);
			if (BitConverter.IsLittleEndian && switchEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}
	}
}
