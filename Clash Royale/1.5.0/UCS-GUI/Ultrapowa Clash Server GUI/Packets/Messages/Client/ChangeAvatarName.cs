namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;
    using CRS.Packets.Commands.Server;

    #endregion

    internal class ChangeAvatarName : Message
    {
        public const ushort PacketID = 10212;

        public bool Bool = true;

        public string Name = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ChangeAvatarName" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public ChangeAvatarName(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Set_Name.
            
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Name = this.Reader.ReadString();
                this.Bool = this.Reader.ReadBoolean();
                Debug.Write("Name: " + this.Name);
                Debug.Write("Bool: " + this.Bool);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            this.Client.GetLevel().GetPlayerAvatar().SetName(this.Name);
            var p = new AvatarNameChangeOkMessage(this.Client);
            p.SetAvatarName(this.Client.GetLevel().GetPlayerAvatar().GetName());
            PacketManager.Send(p);

            PacketManager.Send(new Server_Commands(this.Client) { _Command = PacketManager.Handle(new Name_Change_Callback(this.Client)) });
        }
    }
}