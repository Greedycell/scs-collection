namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class AskForPlayingFacebookFriends : Message
    {
        public const ushort PacketID = 10513;

        public string[] Accounts;

        /// <summary>
        ///     Initialize a new instance of the <see cref="AskForPlayingFacebookFriends" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public AskForPlayingFacebookFriends(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Social_Connection.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                int Count = this.Reader.ReadVInt();
                this.Accounts = new string[Count];
                Debug.Write("Count: " + Count);
                for (int _Index = 0; _Index < Count; _Index++)
                {
                    this.Accounts[_Index] = this.Reader.ReadString();
                Debug.Write("Account Index: " + this.Accounts[_Index]);
                Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
            }
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Friends_List(this.Client));
        }
    }
}