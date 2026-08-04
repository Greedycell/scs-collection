namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    #endregion

    internal class CreateAlliance : Message
    {
        public const ushort PacketID = 14301;

        public string Name = string.Empty;

        public string Description = string.Empty;

        public int Badge;

        public int Origin;

        public int Required_Score;

        public int Type;

        /// <summary>
        ///     Initialize a new instance of the <see cref="CreateAlliance" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public CreateAlliance(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Create_Clan.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Name = this.Reader.ReadString();
                Debug.Write("Name: " + this.Name);
                this.Description = this.Reader.ReadString();
                Debug.Write("Description: " + this.Description);
                this.Badge = this.Reader.ReadVInt();
                Debug.Write("Badge: " + this.Badge);
                this.Badge = this.Reader.ReadVInt();
                Debug.Write("Badge: " + this.Badge);
                this.Type = this.Reader.Read();
                Debug.Write("Type: " + this.Type);
                this.Required_Score = this.Reader.ReadVInt();
                Debug.Write("Required_Score: " + this.Required_Score);
                this.Origin = this.Reader.ReadVInt();
                Debug.Write("Origin: " + this.Origin);
                this.Origin = this.Reader.ReadVInt();
                Debug.Write("Origin: " + this.Origin);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}