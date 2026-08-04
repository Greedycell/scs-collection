namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Friends_List : Message
    {
        public const ushort PacketID = 20105;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Friends_List" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Friends_List(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.AddInt(0);

            this.Writer.AddInt(2); // Count

            // Second Player
            this.Writer.AddLong(2); // ID
            this.Writer.AddLong(2); // ID

            this.Writer.AddString("Aidid"); // Name
            this.Writer.AddString("1083844704999868");
                // App-Scoped FB User ID | E.g : https://graph.facebook.com/v2.2/1083844704999868/picture 
            this.Writer.AddString("G:537371520"); // Gamecenter ID
            this.Writer.AddUShort(12); // Level
            this.Writer.AddVInt(4000);

            this.Writer.Add(1);
            this.Writer.AddLong(2);
            this.Writer.AddString("Mother fuckers"); // Clan Name
            this.Writer.Add(1);
            this.Writer.AddRange("10-A5-02".HexaToBytes()); // Clan Badge

            this.Writer.AddString(null);
            this.Writer.AddString(null);

            // First Player
            this.Writer.AddLong(1); // ID
            this.Writer.AddLong(1); // ID

            this.Writer.AddString("JJBreaker"); // Name
            this.Writer.AddString("222174321456941");
                // App-Scoped FB User ID | E.g : https://graph.facebook.com/v2.2/222174321456941/picture 
            this.Writer.AddString("G:537371521"); // Gamecenter ID
            this.Writer.AddUShort(13); // Level
            this.Writer.AddVInt(5000);

            this.Writer.Add(1);
            this.Writer.AddLong(1);
            this.Writer.AddString("ClashRoyaleSpain"); // Clan Name
            this.Writer.Add(1);
            this.Writer.AddRange("10-A1-02".HexaToBytes()); // Clan Badge

            this.Writer.AddString(null);
            this.Writer.AddString(null);

            // this.Writer.AddRange("00-00-00-01-00-0D-E8-02-00-00-00-01-00-0D-E8-02-00-00-00-05-67-64-64-31-32-00-00-00-0F-39-39-35-35-35-31-30-34-33-38-37-31-30-36-32-FF-FF-FF-FF-00-06-8A-0F-01-00-00-00-00-00-00-83-65-00-00-00-03-73-2E-62-03-10-34-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-02-00-1E-1F-13-00-00-00-02-00-1E-1F-13-00-00-00-09-69-6E-63-6F-6E-69-74-6F-6F-00-00-00-10-31-31-37-38-38-38-31-35-30-38-38-32-33-35-36-37-00-00-00-0B-47-3A-33-31-31-33-37-30-35-36-36-00-04-8B-06-00-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-24-00-7B-EA-E4-00-00-00-24-00-7B-EA-E4-00-00-00-08-4E-65-63-6C-61-73-79-61-00-00-00-10-31-32-38-33-32-37-35-39-37-31-37-30-36-35-35-37-FF-FF-FF-FF-00-06-93-1B-01-00-00-00-08-00-02-B1-94-00-00-00-0A-54-68-65-20-41-67-65-6E-74-73-03-10-8D-01-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-04-00-04-09-22-00-00-00-04-00-04-09-22-00-00-00-08-4D-69-6D-69-38-32-39-38-00-00-00-10-31-36-37-30-33-34-31-33-36-33-32-30-38-30-31-38-00-00-00-0D-47-3A-31-30-31-34-34-34-37-36-33-36-38-00-0A-BF-32-01-00-00-00-01-00-03-D5-C7-00-00-00-0D-49-6E-54-68-65-4C-69-67-68-74-20-46-52-03-10-14-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-05-00-18-5D-C4-00-00-00-05-00-18-5D-C4-00-00-00-07-56-61-6C-6F-78-69-63-00-00-00-10-31-30-33-31-37-37-37-37-32-33-35-34-39-37-35-35-FF-FF-FF-FF-00-05-88-0C-00-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-16-00-0E-85-66-00-00-00-16-00-0E-85-66-00-00-00-08-63-72-65-61-70-72-6F-67-00-00-00-0F-39-31-36-30-38-37-34-32-38-34-38-39-30-37-31-00-00-00-0C-47-3A-38-32-32-36-37-38-32-30-33-35-00-05-A7-0D-01-00-00-00-18-00-00-4E-16-00-00-00-0A-61-6E-67-65-6C-20-66-69-72-65-03-10-2E-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-16-00-3A-C6-DA-00-00-00-16-00-3A-C6-DA-00-00-00-05-4E-61-6E-6F-77-00-00-00-0F-32-37-38-33-37-36-35-33-32-35-30-34-30-38-37-FF-FF-FF-FF-00-06-86-11-01-00-00-00-09-00-00-24-4A-00-00-00-0B-47-6F-62-65-6C-69-6E-4C-61-6E-64-01-10-90-02-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-0A-00-02-85-88-00-00-00-0A-00-02-85-88-00-00-00-04-41-6E-74-7A-00-00-00-11-31-30-32-30-37-36-34-32-39-32-31-34-38-37-36-38-32-FF-FF-FF-FF-00-07-84-12-01-00-00-00-0A-00-00-61-88-00-00-00-0C-44-72-6F-73-20-44-65-6C-6E-6F-63-68-04-10-82-01-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-0C-00-01-90-9E-00-00-00-0C-00-01-90-9E-00-00-00-06-52-6F-6D-61-69-6E-00-00-00-0F-35-33-32-34-35-33-34-30-30-32-35-39-36-34-37-00-00-00-0D-47-3A-31-30-31-39-33-30-31-35-31-33-32-00-09-B2-24-01-00-00-00-06-00-03-DD-1C-00-00-00-0D-23-4E-6F-50-61-69-6E-4E-6F-47-61-6D-65-01-10-89-02-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-1E-00-0B-3B-71-00-00-00-1E-00-0B-3B-71-00-00-00-0B-44-61-72-6B-53-6F-75-6C-5F-59-54-00-00-00-0F-36-38-35-34-36-33-31-32-38-32-36-37-38-36-33-00-00-00-0C-47-3A-31-36-31-36-36-32-32-33-33-31-00-05-B3-14-00-FF-FF-FF-FF-FF-FF-FF-FF-00-00-00-1F-00-27-20-C8-00-00-00-1F-00-27-20-C8-00-00-00-05-47-4D-5F-59-54-00-00-00-0F-32-31-36-34-37-32-35-36-35-33-37-30-30-37-30-FF-FF-FF-FF-00-08-BD-21-01-00-00-00-00-00-01-18-A5-00-00-00-0C-53-69-64-65-42-72-65-61-6B-65-72-73-03-10-9F-01-FF-FF-FF-FF-FF-FF-FF-FF".HexaToBytes());
            
        }
    }
}