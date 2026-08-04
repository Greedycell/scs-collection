namespace CRS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using CRS.Core;
    using CRS.Extensions;
    using CRS.Extensions.Binary;
    //using CRS.Library.Sodium;
    using CRS.Logic;
    using CRS.Logic.Enums;
    using CRS.Utilities.Sodium;

    using SecretBox = Utilities.Sodium.SecretBox;

    #endregion

    internal class Message
    {
        private byte[] m_vData;

        private int m_vLength;

        private ushort m_vMessageVersion;

        private ushort m_vType;

        public int Broadcasting { get; set; }

        public Client Client { get; set; }

        /// <summary>
        /// Get or set the reader.
        /// </summary>
        /// <value>The reader.</value>
        public Reader Reader
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the writer.
        /// </summary>
        /// <value>The writer.</value>
        public List<byte> Writer
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the packet direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction
        {
            get;
            set;
        }

        public Message()
        {
            this.Client = null;

            this.m_vType = 0;
            this.m_vLength = 0;
            this.m_vMessageVersion = 0;

            this.m_vData = null;
            this.Reader = null;
            this.Writer = null;
        }

        public Message(Client c)
        {
            this.Client = c;

            this.m_vType = 0;
            this.m_vLength = 0;
            this.m_vMessageVersion = 0;

            this.m_vData = null;
            this.Reader = null;
            this.Writer = new List<byte>();

            this.Direction = Direction.CLIENT;
        }

        public Message(Client c, Reader _Reader, int[] _Header)
        {
            this.Client = c;

            this.m_vType = (ushort)_Header[0];
            this.m_vLength = (ushort)_Header[1];
            this.m_vMessageVersion = (ushort)_Header[2];

            try
            {
                this.m_vData = _Reader.ReadBytes(_Header[1]);
            }
            catch (IOException ioException)
            {
                Debug.Write(Debug.FlattenException(ioException));
            }
            this.Reader = new Reader(this.m_vData);
            this.Writer = null;

            this.Direction = Direction.SERVER;
        }

        public virtual void Encrypt()
        {
            try
            {
                if (this.Client.State >= (int)State.LOGGED)
                {
                    this.Client.CRNonce.Increment();

                    this.m_vData = Library.Sodium.Sodium.Encrypt(this.Writer.ToArray(), this.Client.CRNonce, this.Client.CPublicKey).Skip(16).ToArray();
                    this.m_vLength = this.m_vData.Length;
                }
                else
                {
                    this.m_vData = this.Writer.ToArray();
                    this.m_vLength = this.m_vData.Length;
                }
            }
            catch (Exception ex)
            {
                this.Client.State = (int)State.DISCONNECTED;
                Debug.Write(Debug.FlattenException(ex));
            }
        }

        public void Encrypt(byte[] plainText)
        {
            try
            {
                if (this.GetMessageType() == 20103)
                {
                    byte[] nonce = CRS.Utilities.Sodium.GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
                    plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
                    this.SetData(CRS.Utilities.Sodium.PublicKeyBox.Create(plainText, nonce, Key.Crypto.PrivateKey, this.Client.CPublicKey));
                }
                else if (this.GetMessageType() == 20104)
                {
                    byte[] nonce2 = CRS.Utilities.Sodium.GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
                    plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
                    this.SetData(CRS.Utilities.Sodium.PublicKeyBox.Create(plainText, nonce2, Key.Crypto.PrivateKey, this.Client.CPublicKey));
                    this.Client.State = 2;
                }
                else
                {
                    this.Client.CRNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CRNonce));
                    this.SetData(SecretBox.Create(plainText, this.Client.CRNonce, this.Client.CSharedKey).Skip(16).ToArray<byte>());
                }
            }
            catch (Exception ex)
            {
                this.Client.State = 0;
                Debug.Write(Debug.FlattenException(ex));
            }
        }
        //public void UpdateEncrypt()
        //{
        //    this.Client.CRNonce = CRS.Utilities.Increment(CRS.Utilities.Increment(this.Client.CRNonce));
        //}

        //public void UpdateDecrypt()
        //{
        //    this.Client.CSNonce = CRS.Utilities.Increment(CRS.Utilities.Increment(this.Client.CSNonce));
        //}

        public virtual void Decrypt()
        {
            try
            {
                if (this.Client.State >= (int)State.LOGGED)
                {
                    this.Client.CSNonce.Increment();

                    this.m_vData = Library.Sodium.Sodium.Decrypt(new byte[16].Concat(this.m_vData).ToArray(), this.Client.CSNonce, this.Client.CPublicKey);
                    this.Reader = new Reader(this.m_vData);
                    this.m_vLength = this.m_vData.Length;
                }
            }
            catch (Exception ex)
            {
                this.Client.State = (int)State.DISCONNECTED;
                Debug.Write(Debug.FlattenException(ex));
            }
        }

        public void Decrypt2()
        {
            try
            {
                if (this.m_vType == 10101)
                {
                    //byte[] array = this.m_vData;
                    //this.Client.CPublicKey = array.Take(32).ToArray<byte>();
                    //this.Client.CSharedKey = this.Client.CPublicKey;
                    //this.Client.CRNonce = Client.GenerateSessionKey();
                    //byte[] nonce = GenericHash.Hash(this.Client.CPublicKey.Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
                    //array = array.Skip(32).ToArray<byte>();
                    //byte[] source = PublicKeyBox.Open(array, nonce, Key.Crypto.PrivateKey, this.Client.CPublicKey);
                    //this.Client.CSessionKey = source.Take(24).ToArray<byte>();
                    //this.Client.CSNonce = source.Skip(24).Take(24).ToArray<byte>();
                    //this.SetData(source.Skip(24).Skip(24).ToArray<byte>());
                    this.Client.CSNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CSNonce));
                    this.SetData(SecretBox.Open(new byte[16].Concat(this.m_vData).ToArray<byte>(), this.Client.CSNonce, this.Client.CSharedKey));
                }
                else
                {
                    this.Client.CSNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CSNonce));
                    this.SetData(SecretBox.Open(new byte[16].Concat(this.m_vData).ToArray<byte>(), this.Client.CSNonce, this.Client.CSharedKey));
                }
            }
            catch (Exception)
            {
                this.Client.State = 0;
            }
        }

        public virtual void Decode()
        {
        }

        public virtual void Encode()
        {
        }

        public byte[] GetData()
        {
            return this.m_vData;
        }

        public int GetLength()
        {
            return this.m_vLength;
        }

        public ushort GetMessageType()
        {
            return this.m_vType;
        }

        public ushort GetMessageVersion()
        {
            return this.m_vMessageVersion;
        }

        public byte[] GetRawData()
        {
            List<byte> expr_05 = new List<byte>();
            expr_05.AddRange(BitConverter.GetBytes(this.m_vType).Reverse());
            expr_05.AddRange(BitConverter.GetBytes(this.m_vLength).Reverse().Skip(1));
            expr_05.AddRange(BitConverter.GetBytes(this.m_vMessageVersion).Reverse());
            expr_05.AddRange(this.m_vData);
            return expr_05.ToArray();
        }

        public virtual void Process(Level level)
        {
        }

        public void SetData(byte[] data)
        {
            this.m_vData = data;
            this.m_vLength = data.Length;
        }

        public void SetMessageType(ushort type)
        {
            this.m_vType = type;
            Debug.Write("Server Message " + type + ": " + PacketTypes.GetPacketTypeByID(type) + "  was set");
        }

        public void SetMessageVersion(ushort v)
        {
            this.m_vMessageVersion = v;
        }

        public string ToHexString()
        {
            return BitConverter.ToString(this.m_vData).Replace("-", " ");
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.m_vData, 0, this.m_vLength);
        }

        public static byte[] AddVInt(int v2)
        {
            MemoryStream memoryStream = new MemoryStream(5);
            if (v2 <= -1)
            {
                if (v2 + 63 < 0)
                {
                    memoryStream.WriteByte((byte)((v2 & 63) | 64));
                    return memoryStream.ToArray();
                }
                if (v2 >= -8191)
                {
                    memoryStream.WriteByte((byte)(v2 | 192));
                    v2 >>= 6;
                    memoryStream.WriteByte((byte)v2);
                    return memoryStream.ToArray();
                }
                if (v2 >= -1048575)
                {
                    memoryStream.WriteByte((byte)(v2 | 192));
                    memoryStream.WriteByte((byte)((v2 >> 6) | 128));
                    v2 >>= 13;
                    memoryStream.WriteByte((byte)v2);
                    return memoryStream.ToArray();
                }
                memoryStream.WriteByte((byte)(v2 | 192));
                memoryStream.WriteByte((byte)((v2 >> 6) | 128));
                memoryStream.WriteByte((byte)((v2 >> 13) | 128));
                v2 >>= 20;
                if (v2 <= -134217728)
                {
                    memoryStream.WriteByte((byte)(v2 | 128));
                    v2 >>= 11;
                    memoryStream.WriteByte((byte)v2);
                    return memoryStream.ToArray();
                }
                memoryStream.WriteByte((byte)(v2 & 127));
                return memoryStream.ToArray();
            }
            if (v2 <= 63)
            {
                v2 &= 63;
                memoryStream.WriteByte((byte)v2);
                return memoryStream.ToArray();
            }
            if (v2 < 8192)
            {
                memoryStream.WriteByte((byte)((v2 & 63) | 128));
                v2 >>= 6;
                memoryStream.WriteByte((byte)v2);
                return memoryStream.ToArray();
            }
            if (v2 < 1048576)
            {
                memoryStream.WriteByte((byte)((v2 & 63) | 128));
                memoryStream.WriteByte((byte)((v2 >> 6) | 128));
                v2 >>= 13;
                memoryStream.WriteByte((byte)v2);
                return memoryStream.ToArray();
            }
            memoryStream.WriteByte((byte)((v2 & 63) | 128));
            memoryStream.WriteByte((byte)((v2 >> 6) | 128));
            memoryStream.WriteByte((byte)((v2 >> 13) | 128));
            v2 >>= 20;
            if (v2 >= 134217728)
            {
                memoryStream.WriteByte((byte)(v2 | 128));
                v2 >>= 11;
                memoryStream.WriteByte((byte)v2);
                return memoryStream.ToArray();
            }
            memoryStream.WriteByte((byte)(v2 & 127));
            return memoryStream.ToArray();
        }
    }
}