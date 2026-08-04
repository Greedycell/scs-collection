using System;
using System.Collections;
using System.IO;
using CRS.Utilities.LZMA.Common;
using CRS.Utilities.LZMA.Compress.LZMA;

namespace CRS.Utilities.LZMA.Compress.LzmaAlone
{
	// Token: 0x02000050 RID: 80
	internal class LzmaAlone
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0001244C File Offset: 0x0001064C
		private static void PrintHelp()
		{
			Console.WriteLine("\nUsage:  LZMA <e|d> [<switches>...] inputFile outputFile\n  e: encode file\n  d: decode file\n  b: Benchmark\n<Switches>\n  -d{N}:  set dictionary - [0, 29], default: 23 (8MB)\n  -fb{N}: set number of fast bytes - [5, 273], default: 128\n  -lc{N}: set number of literal context bits - [0, 8], default: 3\n  -lp{N}: set number of literal pos bits - [0, 4], default: 0\n  -pb{N}: set number of pos bits - [0, 4], default: 2\n  -mf{MF_ID}: set Match Finder: [bt2, bt4], default: bt4\n  -eos:   write End Of Stream marker\n");
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00012458 File Offset: 0x00010658
		private static bool GetNumber(string s, out int v)
		{
			v = 0;
			foreach (char c in s)
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
				v *= 10;
				v += (int)(c - '0');
			}
			return true;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001249E File Offset: 0x0001069E
		private static int IncorrectCommand()
		{
			throw new Exception("Command line error");
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000124AC File Offset: 0x000106AC
		private static int Start(string[] args)
		{
			Console.WriteLine("\nLZMA# 4.61  2008-11-23\n");
			if (args.Length == 0)
			{
				LzmaAlone.PrintHelp();
				return 0;
			}
			SwitchForm[] array = new SwitchForm[13];
			int num = 0;
			array[num++] = new SwitchForm("?", SwitchType.Simple, false);
			array[num++] = new SwitchForm("H", SwitchType.Simple, false);
			array[num++] = new SwitchForm("A", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("D", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("FB", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("LC", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("LP", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("PB", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("MF", SwitchType.UnLimitedPostString, false, 1);
			array[num++] = new SwitchForm("EOS", SwitchType.Simple, false);
			array[num++] = new SwitchForm("SI", SwitchType.Simple, false);
			array[num++] = new SwitchForm("SO", SwitchType.Simple, false);
			array[num++] = new SwitchForm("T", SwitchType.UnLimitedPostString, false, 1);
			Parser parser = new Parser(num);
			try
			{
				parser.ParseStrings(array, args);
			}
			catch
			{
				return LzmaAlone.IncorrectCommand();
			}
			if (parser[0].ThereIs || parser[1].ThereIs)
			{
				LzmaAlone.PrintHelp();
				return 0;
			}
			ArrayList nonSwitchStrings = parser.NonSwitchStrings;
			int num2 = 0;
			if (num2 >= nonSwitchStrings.Count)
			{
				return LzmaAlone.IncorrectCommand();
			}
			string text = (string)nonSwitchStrings[num2++];
			text = text.ToLower();
			bool flag = false;
			int num3 = 2097152;
			if (parser[3].ThereIs)
			{
				int num4;
				if (!LzmaAlone.GetNumber((string)parser[3].PostStrings[0], out num4))
				{
					LzmaAlone.IncorrectCommand();
				}
				num3 = 1 << num4;
				flag = true;
			}
			string text2 = "bt4";
			if (parser[8].ThereIs)
			{
				text2 = (string)parser[8].PostStrings[0];
			}
			text2 = text2.ToLower();
			if (text == "b")
			{
				int num5 = 10;
				if (num2 < nonSwitchStrings.Count && !LzmaAlone.GetNumber((string)nonSwitchStrings[num2++], out num5))
				{
					num5 = 10;
				}
				return LzmaBench.LzmaBenchmark(num5, (uint)num3);
			}
			string text3 = "";
			if (parser[12].ThereIs)
			{
				text3 = (string)parser[12].PostStrings[0];
			}
			bool flag2 = false;
			if (text == "e")
			{
				flag2 = true;
			}
			else if (text == "d")
			{
				flag2 = false;
			}
			else
			{
				LzmaAlone.IncorrectCommand();
			}
			bool thereIs = parser[10].ThereIs;
			bool thereIs2 = parser[11].ThereIs;
			if (thereIs)
			{
				throw new Exception("Not implemeted");
			}
			if (num2 >= nonSwitchStrings.Count)
			{
				LzmaAlone.IncorrectCommand();
			}
			Stream stream = new FileStream((string)nonSwitchStrings[num2++], FileMode.Open, FileAccess.Read);
			if (thereIs2)
			{
				throw new Exception("Not implemeted");
			}
			if (num2 >= nonSwitchStrings.Count)
			{
				LzmaAlone.IncorrectCommand();
			}
			FileStream fileStream = new FileStream((string)nonSwitchStrings[num2++], FileMode.Create, FileAccess.Write);
			FileStream fileStream2 = null;
			if (text3.Length != 0)
			{
				fileStream2 = new FileStream(text3, FileMode.Open, FileAccess.Read);
			}
			if (flag2)
			{
				if (!flag)
				{
					num3 = 8388608;
				}
				int num6 = 2;
				int num7 = 3;
				int num8 = 0;
				int num9 = 2;
				int num10 = 128;
				bool flag3 = parser[9].ThereIs || thereIs;
				if (parser[2].ThereIs && !LzmaAlone.GetNumber((string)parser[2].PostStrings[0], out num9))
				{
					LzmaAlone.IncorrectCommand();
				}
				if (parser[4].ThereIs && !LzmaAlone.GetNumber((string)parser[4].PostStrings[0], out num10))
				{
					LzmaAlone.IncorrectCommand();
				}
				if (parser[5].ThereIs && !LzmaAlone.GetNumber((string)parser[5].PostStrings[0], out num7))
				{
					LzmaAlone.IncorrectCommand();
				}
				if (parser[6].ThereIs && !LzmaAlone.GetNumber((string)parser[6].PostStrings[0], out num8))
				{
					LzmaAlone.IncorrectCommand();
				}
				if (parser[7].ThereIs && !LzmaAlone.GetNumber((string)parser[7].PostStrings[0], out num6))
				{
					LzmaAlone.IncorrectCommand();
				}
				CoderPropID[] array2 = new CoderPropID[]
				{
					CoderPropID.DictionarySize,
					CoderPropID.PosStateBits,
					CoderPropID.LitContextBits,
					CoderPropID.LitPosBits,
					CoderPropID.Algorithm,
					CoderPropID.NumFastBytes,
					CoderPropID.MatchFinder,
					CoderPropID.EndMarker
				};
				object[] array3 = new object[] { num3, num6, num7, num8, num9, num10, text2, flag3 };
				Encoder encoder = new Encoder();
				encoder.SetCoderProperties(array2, array3);
				encoder.WriteCoderProperties(fileStream);
				long num11;
				if (flag3 || thereIs)
				{
					num11 = -1L;
				}
				else
				{
					num11 = stream.Length;
				}
				for (int i = 0; i < 8; i++)
				{
					fileStream.WriteByte((byte)(num11 >> 8 * i));
				}
				if (fileStream2 != null)
				{
					CDoubleStream cdoubleStream = new CDoubleStream();
					cdoubleStream.s1 = fileStream2;
					cdoubleStream.s2 = stream;
					cdoubleStream.fileIndex = 0;
					stream = cdoubleStream;
					long length = fileStream2.Length;
					cdoubleStream.skipSize = 0L;
					if (length > (long)num3)
					{
						cdoubleStream.skipSize = length - (long)num3;
					}
					fileStream2.Seek(cdoubleStream.skipSize, SeekOrigin.Begin);
					encoder.SetTrainSize((uint)(length - cdoubleStream.skipSize));
				}
				encoder.Code(stream, fileStream, -1L, -1L, null);
			}
			else
			{
				if (!(text == "d"))
				{
					throw new Exception("Command Error");
				}
				byte[] array4 = new byte[5];
				if (stream.Read(array4, 0, 5) != 5)
				{
					throw new Exception("input .lzma is too short");
				}
				Decoder decoder = new Decoder();
				decoder.SetDecoderProperties(array4);
				if (fileStream2 != null && !decoder.Train(fileStream2))
				{
					throw new Exception("can't train");
				}
				long num12 = 0L;
				for (int j = 0; j < 8; j++)
				{
					int num13 = stream.ReadByte();
					if (num13 < 0)
					{
						throw new Exception("Can't Read 1");
					}
					num12 |= (long)((long)((ulong)((byte)num13)) << 8 * j);
				}
				long num14 = stream.Length - stream.Position;
				decoder.Code(stream, fileStream, num14, num12, null);
			}
			return 0;
		}

		// Token: 0x020000FC RID: 252
		private enum Key
		{
			// Token: 0x0400046F RID: 1135
			Help1,
			// Token: 0x04000470 RID: 1136
			Help2,
			// Token: 0x04000471 RID: 1137
			Mode,
			// Token: 0x04000472 RID: 1138
			Dictionary,
			// Token: 0x04000473 RID: 1139
			FastBytes,
			// Token: 0x04000474 RID: 1140
			LitContext,
			// Token: 0x04000475 RID: 1141
			LitPos,
			// Token: 0x04000476 RID: 1142
			PosBits,
			// Token: 0x04000477 RID: 1143
			MatchFinder,
			// Token: 0x04000478 RID: 1144
			EOS,
			// Token: 0x04000479 RID: 1145
			StdIn,
			// Token: 0x0400047A RID: 1146
			StdOut,
			// Token: 0x0400047B RID: 1147
			Train
		}
	}
}
