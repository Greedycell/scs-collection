using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200000E RID: 14
	public class ParallelDeflateOutputStream : Stream
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000854A File Offset: 0x0000674A
		public ParallelDeflateOutputStream(Stream stream)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008556 File Offset: 0x00006756
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level)
			: this(stream, level, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008562 File Offset: 0x00006762
		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000856E File Offset: 0x0000676E
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen)
			: this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000857C File Offset: 0x0000677C
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			this._outStream = stream;
			this._compressLevel = level;
			this.Strategy = strategy;
			this._leaveOpen = leaveOpen;
			this.MaxBufferPairs = 16;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000085EB File Offset: 0x000067EB
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000085F3 File Offset: 0x000067F3
		public CompressionStrategy Strategy { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000085FC File Offset: 0x000067FC
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00008604 File Offset: 0x00006804
		public int MaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00008621 File Offset: 0x00006821
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00008629 File Offset: 0x00006829
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				this._bufferSize = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000864A File Offset: 0x0000684A
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00008652 File Offset: 0x00006852
		public int Crc32 { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000865B File Offset: 0x0000685B
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00008663 File Offset: 0x00006863
		public long BytesProcessed { get; private set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x0000866C File Offset: 0x0000686C
		private void _InitializePoolOfWorkItems()
		{
			this._toWrite = new Queue<int>();
			this._toFill = new Queue<int>();
			this._pool = new List<WorkItem>();
			int num = ParallelDeflateOutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this._maxBufferPairs);
			for (int i = 0; i < num; i++)
			{
				this._pool.Add(new WorkItem(this._bufferSize, this._compressLevel, this.Strategy, i));
				this._toFill.Enqueue(i);
			}
			this._newlyCompressedBlob = new AutoResetEvent(false);
			this._runningCrc = new CRC32();
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008724 File Offset: 0x00006924
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this._pendingException != null)
			{
				this._handlingException = true;
				object pendingException = this._pendingException;
				this._pendingException = null;
			}
			if (count == 0)
			{
				return;
			}
			if (!this._firstWriteDone)
			{
				this._InitializePoolOfWorkItems();
				this._firstWriteDone = true;
			}
			for (; ; )
			{
				this.EmitPendingBuffers(false, flag);
				flag = false;
				int num;
				if (this._currentlyFilling >= 0)
				{
					num = this._currentlyFilling;
					goto IL_0098;
				}
				if (this._toFill.Count != 0)
				{
					num = this._toFill.Dequeue();
					this._lastFilled++;
					goto IL_0098;
				}
				flag = true;
			IL_0145:
				if (count <= 0)
				{
					return;
				}
				continue;
			IL_0098:
				WorkItem workItem = this._pool[num];
				int num2 = ((workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable));
				workItem.ordinal = this._lastFilled;
				Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
				count -= num2;
				offset += num2;
				workItem.inputBytesAvailable += num2;
				if (workItem.inputBytesAvailable == workItem.buffer.Length)
				{
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeflateOne), workItem))
					{
						break;
					}
					this._currentlyFilling = -1;
				}
				else
				{
					this._currentlyFilling = num;
				}
				goto IL_0145;
			}
			throw new Exception("Cannot enqueue workitem");
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008880 File Offset: 0x00006A80
		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(this._compressLevel, false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			if (num != 1 && num != 0)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			if (array.Length - zlibCodec.AvailableBytesOut > 0)
			{
				this._outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			this.Crc32 = this._runningCrc.Crc32Result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000893C File Offset: 0x00006B3C
		private void _Flush(bool lastInput)
		{
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this.emitting)
			{
				return;
			}
			if (this._currentlyFilling >= 0)
			{
				WorkItem workItem = this._pool[this._currentlyFilling];
				this._DeflateOne(workItem);
				this._currentlyFilling = -1;
			}
			if (lastInput)
			{
				this.EmitPendingBuffers(true, false);
				this._FlushFinish();
				return;
			}
			this.EmitPendingBuffers(false, false);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000089A3 File Offset: 0x00006BA3
		public override void Flush()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				object pendingException = this._pendingException;
				this._pendingException = null;
			}
			if (this._handlingException)
			{
				return;
			}
			this._Flush(false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000089D8 File Offset: 0x00006BD8
		public override void Close()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				object pendingException = this._pendingException;
				this._pendingException = null;
			}
			if (this._handlingException)
			{
				return;
			}
			if (this._isClosed)
			{
				return;
			}
			this._Flush(true);
			if (!this._leaveOpen)
			{
				this._outStream.Close();
			}
			this._isClosed = true;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008A3B File Offset: 0x00006C3B
		public new void Dispose()
		{
			this.Close();
			this._pool = null;
			this.Dispose(true);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008A51 File Offset: 0x00006C51
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008A5C File Offset: 0x00006C5C
		public void Reset(Stream stream)
		{
			if (!this._firstWriteDone)
			{
				return;
			}
			this._toWrite.Clear();
			this._toFill.Clear();
			foreach (WorkItem workItem in this._pool)
			{
				this._toFill.Enqueue(workItem.index);
				workItem.ordinal = -1;
			}
			this._firstWriteDone = false;
			this.BytesProcessed = 0L;
			this._runningCrc = new CRC32();
			this._isClosed = false;
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
			this._outStream = stream;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008B24 File Offset: 0x00006D24
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (this.emitting)
			{
				return;
			}
			this.emitting = true;
			if (doAll || mustWait)
			{
				this._newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = (doAll ? 200 : (mustWait ? (-1) : 0));
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(this._toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (this._toWrite.Count > 0)
							{
								num3 = this._toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(this._toWrite);
						}
						if (num3 >= 0)
						{
							WorkItem workItem = this._pool[num3];
							if (workItem.ordinal != this._lastWritten + 1)
							{
								Queue<int> toWrite = this._toWrite;
								lock (toWrite)
								{
									this._toWrite.Enqueue(num3);
								}
								if (num == num3)
								{
									this._newlyCompressedBlob.WaitOne();
									num = -1;
								}
								else if (num == -1)
								{
									num = num3;
								}
							}
							else
							{
								num = -1;
								this._outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
								this._runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
								this.BytesProcessed += (long)workItem.inputBytesAvailable;
								workItem.inputBytesAvailable = 0;
								this._lastWritten = workItem.ordinal;
								this._toFill.Enqueue(workItem.index);
								if (num2 == -1)
								{
									num2 = 0;
								}
							}
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && this._lastWritten != this._latestCompressed);
			this.emitting = false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008CC4 File Offset: 0x00006EC4
		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				CRC32 crc = new CRC32();
				crc.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				this.DeflateOneSegment(workItem);
				workItem.crc = crc.Crc32Result;
				object obj = this._latestLock;
				lock (obj)
				{
					if (workItem.ordinal > this._latestCompressed)
					{
						this._latestCompressed = workItem.ordinal;
					}
				}
				Queue<int> toWrite = this._toWrite;
				lock (toWrite)
				{
					this._toWrite.Enqueue(workItem.index);
				}
				this._newlyCompressedBlob.Set();
			}
			catch (Exception ex)
			{
				object obj = this._eLock;
				lock (obj)
				{
					if (this._pendingException != null)
					{
						this._pendingException = ex;
					}
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00008DE4 File Offset: 0x00006FE4
		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate();
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008E58 File Offset: 0x00007058
		[Conditional("Trace")]
		private void TraceOutput(ParallelDeflateOutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this._DesiredTrace) != ParallelDeflateOutputStream.TraceBits.None)
			{
				object outputLock = this._outputLock;
				lock (outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
					Console.Write("{0:000} PDOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000475F File Offset: 0x0000295F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000475F File Offset: 0x0000295F
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00008ED0 File Offset: 0x000070D0
		public override bool CanWrite
		{
			get
			{
				return this._outStream.CanWrite;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00008EE4 File Offset: 0x000070E4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Position
		{
			get
			{
				return this._outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008EDD File Offset: 0x000070DD
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008EDD File Offset: 0x000070DD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040000B8 RID: 184
		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		// Token: 0x040000B9 RID: 185
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x040000BA RID: 186
		private List<WorkItem> _pool;

		// Token: 0x040000BB RID: 187
		private readonly bool _leaveOpen;

		// Token: 0x040000BC RID: 188
		private bool emitting;

		// Token: 0x040000BD RID: 189
		private Stream _outStream;

		// Token: 0x040000BE RID: 190
		private int _maxBufferPairs;

		// Token: 0x040000BF RID: 191
		private int _bufferSize = ParallelDeflateOutputStream.IO_BUFFER_SIZE_DEFAULT;

		// Token: 0x040000C0 RID: 192
		private AutoResetEvent _newlyCompressedBlob;

		// Token: 0x040000C1 RID: 193
		private readonly object _outputLock = new object();

		// Token: 0x040000C2 RID: 194
		private bool _isClosed;

		// Token: 0x040000C3 RID: 195
		private bool _firstWriteDone;

		// Token: 0x040000C4 RID: 196
		private int _currentlyFilling;

		// Token: 0x040000C5 RID: 197
		private int _lastFilled;

		// Token: 0x040000C6 RID: 198
		private int _lastWritten;

		// Token: 0x040000C7 RID: 199
		private int _latestCompressed;

		// Token: 0x040000C8 RID: 200
		private CRC32 _runningCrc;

		// Token: 0x040000C9 RID: 201
		private readonly object _latestLock = new object();

		// Token: 0x040000CA RID: 202
		private Queue<int> _toWrite;

		// Token: 0x040000CB RID: 203
		private Queue<int> _toFill;

		// Token: 0x040000CC RID: 204
		private readonly CompressionLevel _compressLevel;

		// Token: 0x040000CD RID: 205
		private volatile Exception _pendingException;

		// Token: 0x040000CE RID: 206
		private bool _handlingException;

		// Token: 0x040000CF RID: 207
		private readonly object _eLock = new object();

		// Token: 0x040000D0 RID: 208
		private readonly ParallelDeflateOutputStream.TraceBits _DesiredTrace = ParallelDeflateOutputStream.TraceBits.Compress | ParallelDeflateOutputStream.TraceBits.EmitAll | ParallelDeflateOutputStream.TraceBits.EmitEnter | ParallelDeflateOutputStream.TraceBits.Session | ParallelDeflateOutputStream.TraceBits.WriteEnter | ParallelDeflateOutputStream.TraceBits.WriteTake;

		// Token: 0x020000CA RID: 202
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000382 RID: 898
			All = 4294967295U,
			// Token: 0x04000383 RID: 899
			Compress = 2048U,
			// Token: 0x04000384 RID: 900
			EmitAll = 58U,
			// Token: 0x04000385 RID: 901
			EmitBegin = 8U,
			// Token: 0x04000386 RID: 902
			EmitDone = 16U,
			// Token: 0x04000387 RID: 903
			EmitEnter = 4U,
			// Token: 0x04000388 RID: 904
			EmitLock = 2U,
			// Token: 0x04000389 RID: 905
			EmitSkip = 32U,
			// Token: 0x0400038A RID: 906
			Flush = 64U,
			// Token: 0x0400038B RID: 907
			Instance = 1024U,
			// Token: 0x0400038C RID: 908
			Lifecycle = 128U,
			// Token: 0x0400038D RID: 909
			None = 0U,
			// Token: 0x0400038E RID: 910
			NotUsed1 = 1U,
			// Token: 0x0400038F RID: 911
			Session = 256U,
			// Token: 0x04000390 RID: 912
			Synch = 512U,
			// Token: 0x04000391 RID: 913
			Write = 4096U,
			// Token: 0x04000392 RID: 914
			WriteEnter = 8192U,
			// Token: 0x04000393 RID: 915
			WriteTake = 16384U
		}
	}
}