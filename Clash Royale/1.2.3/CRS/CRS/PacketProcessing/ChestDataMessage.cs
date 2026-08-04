using System;
using System.Collections.Generic;
using System.Threading;

namespace UCS.PacketProcessing
{
	// Token: 0x02000077 RID: 119
	internal class ChestDataMessage : Message
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0001113F File Offset: 0x0000F33F
		public ChestDataMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24111);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011220 File Offset: 0x0000F420
		private byte[] GenRandomTroop(byte type, int count)
		{
			List<byte> list = new List<byte>();
			int num = new Random().Next(0, 28);
			int num2 = new Random().Next(0, 9);
			int num3 = new Random().Next(1, 150);
			list.Add(type);
			if (type == 26)
			{
				if (num == 23 || num == 25 || num == 26)
				{
					list.Add((byte)num2);
				}
				else
				{
					list.Add((byte)num);
				}
			}
			else
			{
				list.Add((byte)num2);
			}
			list.Add(0);
			list.AddRange(new byte[] { 134, 238, 155, 22 });
			list.AddRange(Message.AddVInt(num3));
			Message.AddVInt(count);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			Thread.Sleep(187);
			return list.ToArray();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000112F0 File Offset: 0x0000F4F0
		private byte[] GenRandomLegendary(int count)
		{
			List<byte> list = new List<byte>();
			bool flag = new Random().Next(0, 2) != 0;
			new Random().Next(3, 150);
			list.Add(26);
			if (!flag)
			{
				list.Add(23);
			}
			else
			{
				list.Add(26);
			}
			list.Add(0);
			list.AddRange(new byte[] { 134, 238, 155, 22 });
			list.Add(1);
			Message.AddVInt(count);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			return list.ToArray();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011384 File Offset: 0x0000F584
		public override void Encode()
		{
			int num = 7;
			int num2 = new Random().Next(0, 10);
			List<byte> list = new List<byte>();
			int num3 = new Random().Next(26, 28);
			list.AddRange(new byte[] { 149, 3, 1 });
			list.Add((byte)num);
			list.AddRange(this.GenRandomTroop(26, num));
			list.AddRange(this.GenRandomTroop(27, num));
			list.AddRange(this.GenRandomTroop(26, num));
			list.AddRange(this.GenRandomTroop((byte)num3, num));
			list.AddRange(this.GenRandomTroop(26, num));
			list.AddRange(this.GenRandomTroop(28, num));
			if (num2 >= 9)
			{
				list.AddRange(this.GenRandomLegendary(num));
			}
			else
			{
				list.AddRange(this.GenRandomTroop(26, num));
			}
			list.AddRange(Message.AddVInt(new Random().Next(4560, 5700)));
			list.AddRange(new byte[] { 0, 1, 4, 11, 127, 127, 0, 0 });
			base.Encrypt(list.ToArray());
		}
	}
}
