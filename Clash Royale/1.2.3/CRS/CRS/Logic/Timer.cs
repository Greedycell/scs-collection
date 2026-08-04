using System;

namespace UCS.Logic
{
	// Token: 0x020000A3 RID: 163
	internal class Timer
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x000143CB File Offset: 0x000125CB
		public Timer()
		{
			this.m_vStartTime = new DateTime(1970, 1, 1);
			this.m_vSeconds = 0;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000143EC File Offset: 0x000125EC
		public void FastForward(int seconds)
		{
			this.m_vSeconds -= seconds;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000143FC File Offset: 0x000125FC
		public int GetRemainingSeconds(DateTime time, bool boost = false, DateTime boostEndTime = default(DateTime), float multiplier = 0f)
		{
			int num;
			if (!boost)
			{
				num = this.m_vSeconds - (int)time.Subtract(this.m_vStartTime).TotalSeconds;
			}
			else if (boostEndTime >= time)
			{
				num = this.m_vSeconds - (int)(time.Subtract(this.m_vStartTime).TotalSeconds * (double)multiplier);
			}
			else
			{
				float num2 = (float)time.Subtract(this.m_vStartTime).TotalSeconds - (float)(time - boostEndTime).TotalSeconds;
				float num3 = (float)time.Subtract(this.m_vStartTime).TotalSeconds - num2;
				num = this.m_vSeconds - (int)(num2 * multiplier + num3);
			}
			if (num <= 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000144B8 File Offset: 0x000126B8
		public int GetRemainingSeconds(DateTime time)
		{
			int num = this.m_vSeconds - (int)time.Subtract(this.m_vStartTime).TotalSeconds;
			if (num <= 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000144EA File Offset: 0x000126EA
		public DateTime GetStartTime()
		{
			return this.m_vStartTime;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000144F2 File Offset: 0x000126F2
		public void StartTimer(int seconds, DateTime time)
		{
			this.m_vStartTime = time;
			this.m_vSeconds = seconds;
		}

		// Token: 0x040002C2 RID: 706
		private int m_vSeconds;

		// Token: 0x040002C3 RID: 707
		private DateTime m_vStartTime;
	}
}
