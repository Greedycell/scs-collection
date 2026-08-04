using System;
using System.Collections.Generic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000080 RID: 128
	internal class ChestDataMessage : Message
	{
		// Token: 0x0600037F RID: 895 RVA: 0x000193E9 File Offset: 0x000175E9
		public ChestDataMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24111);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001941B File Offset: 0x0001761B
		public void UseEpicCount(int value)
		{
			this.m_vEpicCount -= value;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001942B File Offset: 0x0001762B
		public void UseRareCount(int value)
		{
			this.m_vRareCount -= value;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001943B File Offset: 0x0001763B
		public void UseCommunCount(int value)
		{
			this.m_vCommunCount -= value;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001944B File Offset: 0x0001764B
		public int EpicCount()
		{
			return this.m_vEpicCount;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00019453 File Offset: 0x00017653
		public int RareCount()
		{
			return this.m_vRareCount;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001945B File Offset: 0x0001765B
		public int CommunCount()
		{
			return this.m_vCommunCount;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00019463 File Offset: 0x00017663
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0001946B File Offset: 0x0001766B
		private byte UnitType { get; set; }

		// Token: 0x06000388 RID: 904 RVA: 0x00019474 File Offset: 0x00017674
		public byte[] GenerateCard(byte Count, int Type, int GenerateModeType)
		{
			List<byte> list = new List<byte>();
			int num = new Random().Next(0, 20);
			if (num >= 15)
			{
				this.UnitType = 28;
				list.Add(this.UnitType);
			}
			else if (num >= 8)
			{
				this.UnitType = 27;
				list.Add(this.UnitType);
			}
			else if (num >= 0)
			{
				this.UnitType = 26;
				list.Add(this.UnitType);
			}
			if (Type == 1)
			{
				if (this.UnitType == 26)
				{
					int num2 = new Random().Next(0, 100);
					if (num2 >= 90)
					{
						list.Add(25);
					}
					if (num2 >= 80)
					{
						list.Add(4);
					}
					else if (num2 >= 70)
					{
						list.Add(6);
					}
					else if (num2 >= 60)
					{
						list.Add(7);
					}
					else if (num2 >= 50)
					{
						list.Add(9);
					}
					else if (num2 >= 40)
					{
						list.Add(12);
					}
					else if (num2 >= 30)
					{
						list.Add(15);
					}
					else if (num2 >= 20)
					{
						list.Add(16);
					}
					else if (num2 >= 10)
					{
						list.Add(20);
					}
					else if (num2 >= 0)
					{
						list.Add(27);
					}
				}
				else if (this.UnitType == 27)
				{
					list.Add(8);
				}
				else if (this.UnitType == 28)
				{
					int num3 = new Random().Next(0, 60);
					if (num3 >= 50)
					{
						list.Add(2);
					}
					else if (num3 >= 40)
					{
						list.Add(4);
					}
					else if (num3 >= 30)
					{
						list.Add(5);
					}
					else if (num3 >= 20)
					{
						list.Add(6);
					}
					else if (num3 >= 10)
					{
						list.Add(7);
					}
					else if (num3 >= 0)
					{
						list.Add(9);
					}
				}
				list.Add(0);
				list.AddRange(new byte[] { 134, 238, 155, 22 });
				if (GenerateModeType == 1)
				{
					int num4 = new Random().Next(3, 9);
					list.AddRange(Message.AddVInt(num4));
					this.UseEpicCount(num4);
				}
				else
				{
					list.AddRange(Message.AddVInt(this.EpicCount()));
				}
				list.Add(0);
				list.Add(0);
				list.Add(1);
			}
			if (Type == 2)
			{
				if (this.UnitType == 26)
				{
					int num5 = new Random().Next(0, 60);
					if (num5 >= 50)
					{
						list.Add(3);
					}
					else if (num5 >= 40)
					{
						list.Add(11);
					}
					else if (num5 >= 30)
					{
						list.Add(14);
					}
					else if (num5 >= 20)
					{
						list.Add(17);
					}
					else if (num5 >= 10)
					{
						list.Add(18);
					}
					else if (num5 >= 0)
					{
						list.Add(21);
					}
				}
				else if (this.UnitType == 27)
				{
					int num6 = new Random().Next(0, 70);
					if (num6 >= 60)
					{
						list.Add(10);
					}
					else if (num6 >= 50)
					{
						list.Add(3);
					}
					else if (num6 >= 40)
					{
						list.Add(4);
					}
					else if (num6 >= 30)
					{
						list.Add(1);
					}
					else if (num6 >= 20)
					{
						list.Add(5);
					}
					else if (num6 >= 10)
					{
						list.Add(9);
					}
					else if (num6 >= 0)
					{
						list.Add(7);
					}
				}
				else if (this.UnitType == 28)
				{
					int num7 = new Random().Next(0, 20);
					if (num7 >= 10)
					{
						list.Add(0);
					}
					else if (num7 >= 0)
					{
						list.Add(3);
					}
				}
				list.Add(0);
				list.AddRange(new byte[] { 134, 238, 155, 22 });
				if (GenerateModeType == 1)
				{
					int num8 = new Random().Next(30, 60);
					list.AddRange(Message.AddVInt(num8));
					this.UseRareCount(num8);
				}
				else
				{
					list.AddRange(Message.AddVInt(this.RareCount()));
				}
				list.Add(0);
				list.Add(0);
				list.Add(1);
			}
			if (Type == 3)
			{
				if (this.UnitType == 26)
				{
					int num9 = new Random().Next(0, 110);
					if (num9 >= 100)
					{
						list.Add(31);
					}
					else if (num9 >= 90)
					{
						list.Add(0);
					}
					else if (num9 >= 80)
					{
						list.Add(1);
					}
					else if (num9 >= 70)
					{
						list.Add(2);
					}
					else if (num9 >= 60)
					{
						list.Add(5);
					}
					else if (num9 >= 50)
					{
						list.Add(8);
					}
					else if (num9 >= 40)
					{
						list.Add(10);
					}
					else if (num9 >= 30)
					{
						list.Add(13);
					}
					else if (num9 >= 20)
					{
						list.Add(19);
					}
					else if (num9 >= 10)
					{
						list.Add(22);
					}
					else if (num9 >= 0)
					{
						list.Add(24);
					}
				}
				else if (this.UnitType == 27)
				{
					int num10 = new Random().Next(0, 30);
					if (num10 >= 20)
					{
						list.Add(0);
					}
					else if (num10 >= 10)
					{
						list.Add(2);
					}
					else if (num10 >= 0)
					{
						list.Add(6);
					}
				}
				else if (this.UnitType == 28)
				{
					int num11 = new Random().Next(0, 30);
					if (num11 >= 10)
					{
						list.Add(1);
					}
					else if (num11 >= 0)
					{
						list.Add(8);
					}
				}
				list.Add(0);
				list.AddRange(new byte[] { 134, 238, 155, 22 });
				if (GenerateModeType == 1)
				{
					int num12 = new Random().Next(50, 134);
					list.AddRange(Message.AddVInt(num12));
					this.UseCommunCount(num12);
				}
				else
				{
					list.AddRange(Message.AddVInt(this.CommunCount()));
				}
				list.Add(0);
				list.Add(0);
				list.Add(1);
			}
			return list.ToArray();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00019A38 File Offset: 0x00017C38
		public byte[] GenerateLegendary()
		{
			List<byte> list = new List<byte>();
			int num = new Random().Next(0, 30);
			list.Add(26);
			if (num >= 25)
			{
				list.Add(33);
			}
			else if (num >= 20)
			{
				list.Add(32);
			}
			else if (num >= 15)
			{
				list.Add(29);
			}
			else if (num >= 10)
			{
				list.Add(23);
			}
			else if (num >= 5)
			{
				list.Add(23);
			}
			else if (num >= 0)
			{
				list.Add(26);
			}
			list.Add(0);
			list.AddRange(new byte[] { 134, 238, 155, 22 });
			list.Add(1);
			list.Add(0);
			list.Add(0);
			list.Add(1);
			return list.ToArray();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public override void Encode()
		{
			byte b = 7;
			new Random().Next(0, 10);
			List<byte> list = new List<byte>();
			int num = new Random().Next(0, 6);
			list.AddRange(new byte[] { 149, 3, 1 });
			list.Add(b);
			if (num == 0)
			{
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 1, 1));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 2, 2));
				list.AddRange(this.GenerateCard(b, 1, 2));
			}
			else if (num == 1)
			{
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 1, 1));
				list.AddRange(this.GenerateCard(b, 1, 2));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 2, 2));
			}
			else if (num == 2)
			{
				list.AddRange(this.GenerateCard(b, 1, 1));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 2, 2));
				list.AddRange(this.GenerateCard(b, 1, 2));
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateLegendary());
			}
			else if (num == 3)
			{
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 2));
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 1, 2));
			}
			else if (num == 4)
			{
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 3, 1));
				list.AddRange(this.GenerateCard(b, 2, 2));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 1, 2));
				list.AddRange(this.GenerateLegendary());
			}
			else if (num == 5)
			{
				list.AddRange(this.GenerateCard(b, 2, 1));
				list.AddRange(this.GenerateCard(b, 1, 1));
				list.AddRange(this.GenerateCard(b, 1, 2));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 3, 2));
				list.AddRange(this.GenerateCard(b, 2, 2));
				list.AddRange(this.GenerateLegendary());
			}
			list.AddRange(Message.AddVInt(new Random().Next(3560, 4650)));
			list.AddRange(Message.AddVInt(new Random().Next(500, 1000)));
			list.AddRange(new byte[] { 1, 1, 11, 127, 127, 0, 0 });
			base.Encrypt(list.ToArray());
		}

		// Token: 0x040002CD RID: 717
		private int m_vEpicCount = 13;

		// Token: 0x040002CE RID: 718
		private int m_vRareCount = 200;

		// Token: 0x040002CF RID: 719
		private int m_vCommunCount = 400;
	}
}
