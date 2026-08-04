using System;
using System.Collections;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000055 RID: 85
	public class Parser
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00012FF4 File Offset: 0x000111F4
		public Parser(int numSwitches)
		{
			this._switches = new SwitchResult[numSwitches];
			for (int i = 0; i < numSwitches; i++)
			{
				this._switches[i] = new SwitchResult();
			}
		}

		// Token: 0x1700005B RID: 91
		public SwitchResult this[int index]
		{
			get
			{
				return this._switches[index];
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00013044 File Offset: 0x00011244
		private bool ParseString(string srcString, SwitchForm[] switchForms)
		{
			int length = srcString.Length;
			if (length == 0)
			{
				return false;
			}
			int i = 0;
			if (!Parser.IsItSwitchChar(srcString[i]))
			{
				return false;
			}
			while (i < length)
			{
				if (Parser.IsItSwitchChar(srcString[i]))
				{
					i++;
				}
				int num = 0;
				int num2 = -1;
				for (int j = 0; j < this._switches.Length; j++)
				{
					int length2 = switchForms[j].IDString.Length;
					if (length2 > num2 && i + length2 <= length && string.Compare(switchForms[j].IDString, 0, srcString, i, length2, true) == 0)
					{
						num = j;
						num2 = length2;
					}
				}
				if (num2 == -1)
				{
					throw new Exception("maxLen == kNoLen");
				}
				SwitchResult switchResult = this._switches[num];
				SwitchForm switchForm = switchForms[num];
				if (!switchForm.Multi && switchResult.ThereIs)
				{
					throw new Exception("switch must be single");
				}
				switchResult.ThereIs = true;
				i += num2;
				int num3 = length - i;
				SwitchType type = switchForm.Type;
				switch (type)
				{
				case SwitchType.PostMinus:
					if (num3 == 0)
					{
						switchResult.WithMinus = false;
					}
					else
					{
						switchResult.WithMinus = srcString[i] == '-';
						if (switchResult.WithMinus)
						{
							i++;
						}
					}
					break;
				case SwitchType.LimitedPostString:
				case SwitchType.UnLimitedPostString:
				{
					int minLen = switchForm.MinLen;
					if (num3 < minLen)
					{
						throw new Exception("switch is not full");
					}
					if (type == SwitchType.UnLimitedPostString)
					{
						switchResult.PostStrings.Add(srcString.Substring(i));
						return true;
					}
					string text = srcString.Substring(i, minLen);
					i += minLen;
					int num4 = minLen;
					while (num4 < switchForm.MaxLen && i < length)
					{
						char c = srcString[i];
						if (Parser.IsItSwitchChar(c))
						{
							break;
						}
						text += c.ToString();
						num4++;
						i++;
					}
					switchResult.PostStrings.Add(text);
					break;
				}
				case SwitchType.PostChar:
				{
					if (num3 < switchForm.MinLen)
					{
						throw new Exception("switch is not full");
					}
					string postCharSet = switchForm.PostCharSet;
					if (num3 == 0)
					{
						switchResult.PostCharIndex = -1;
					}
					else
					{
						int num5 = postCharSet.IndexOf(srcString[i]);
						if (num5 < 0)
						{
							switchResult.PostCharIndex = -1;
						}
						else
						{
							switchResult.PostCharIndex = num5;
							i++;
						}
					}
					break;
				}
				}
			}
			return true;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00013284 File Offset: 0x00011484
		public void ParseStrings(SwitchForm[] switchForms, string[] commandStrings)
		{
			int num = commandStrings.Length;
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				string text = commandStrings[i];
				if (flag)
				{
					this.NonSwitchStrings.Add(text);
				}
				else if (text == "--")
				{
					flag = true;
				}
				else if (!this.ParseString(text, switchForms))
				{
					this.NonSwitchStrings.Add(text);
				}
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000132E4 File Offset: 0x000114E4
		public static int ParseCommand(CommandForm[] commandForms, string commandString, out string postString)
		{
			for (int i = 0; i < commandForms.Length; i++)
			{
				string idstring = commandForms[i].IDString;
				if (commandForms[i].PostStringMode)
				{
					if (commandString.IndexOf(idstring) == 0)
					{
						postString = commandString.Substring(idstring.Length);
						return i;
					}
				}
				else if (commandString == idstring)
				{
					postString = "";
					return i;
				}
			}
			postString = "";
			return -1;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00013348 File Offset: 0x00011548
		private static bool ParseSubCharsCommand(int numForms, CommandSubCharsSet[] forms, string commandString, ArrayList indices)
		{
			indices.Clear();
			int num = 0;
			for (int i = 0; i < numForms; i++)
			{
				CommandSubCharsSet commandSubCharsSet = forms[i];
				int num2 = -1;
				int length = commandSubCharsSet.Chars.Length;
				for (int j = 0; j < length; j++)
				{
					char c = commandSubCharsSet.Chars[j];
					int num3 = commandString.IndexOf(c);
					if (num3 >= 0)
					{
						if (num2 >= 0)
						{
							return false;
						}
						if (commandString.IndexOf(c, num3 + 1) >= 0)
						{
							return false;
						}
						num2 = j;
						num++;
					}
				}
				if (num2 == -1 && !commandSubCharsSet.EmptyAllowed)
				{
					return false;
				}
				indices.Add(num2);
			}
			return num == commandString.Length;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000133F0 File Offset: 0x000115F0
		private static bool IsItSwitchChar(char c)
		{
			return c == '-' || c == '/';
		}

		// Token: 0x04000262 RID: 610
		private const char kSwitchID1 = '-';

		// Token: 0x04000263 RID: 611
		private const char kSwitchID2 = '/';

		// Token: 0x04000264 RID: 612
		private const char kSwitchMinus = '-';

		// Token: 0x04000265 RID: 613
		private const string kStopSwitchParsing = "--";

		// Token: 0x04000266 RID: 614
		private readonly SwitchResult[] _switches;

		// Token: 0x04000267 RID: 615
		public ArrayList NonSwitchStrings = new ArrayList();
	}
}
