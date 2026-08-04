using System;
using System.Text;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200001F RID: 31
	public class Iso8859Dash1Encoding : Encoding
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00009AFE File Offset: 0x00007CFE
		public static int CharacterCount
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00009B05 File Offset: 0x00007D05
		public override string WebName
		{
			get
			{
				return "iso-8859-1";
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009B0C File Offset: 0x00007D0C
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009B10 File Offset: 0x00007D10
		public override int GetBytes(char[] chars, int start, int count, byte[] bytes, int byteIndex)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "null array");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "null array");
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (chars.Length - start < count)
			{
				throw new ArgumentOutOfRangeException("chars");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex");
			}
			for (int i = 0; i < count; i++)
			{
				char c = chars[start + i];
				if (c >= 'ÿ')
				{
					bytes[byteIndex + i] = 63;
				}
				else
				{
					bytes[byteIndex + i] = (byte)c;
				}
			}
			return count;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00009B0C File Offset: 0x00007D0C
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009BBC File Offset: 0x00007DBC
		public override int GetChars(byte[] bytes, int start, int count, char[] chars, int charIndex)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "null array");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "null array");
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (bytes.Length - start < count)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex");
			}
			for (int i = 0; i < count; i++)
			{
				chars[charIndex + i] = (char)bytes[i + start];
			}
			return count;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009C52 File Offset: 0x00007E52
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009C52 File Offset: 0x00007E52
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
