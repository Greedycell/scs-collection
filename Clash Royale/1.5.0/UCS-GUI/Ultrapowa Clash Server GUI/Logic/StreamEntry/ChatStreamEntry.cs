using System.Collections.Generic;

namespace CRS.Logic
{
    using CRS.Extensions.List;

    class ChatStreamEntry : StreamEntry
    {
        private string m_vMessage;
        
        public ChatStreamEntry() : base()
        {
        }

        public string GetMessage()
        {
            return this.m_vMessage;
        }

        public override int GetStreamEntryType()
        {
            return 2;
        }

        public override byte[] Encode()
        {
            List<byte> data = new List<byte>();

            data.AddRange(base.Encode());
            data.AddString(this.m_vMessage);

            return data.ToArray();
        }

        public void SetMessage(string message)
        {
            this.m_vMessage = message;
        }
    }    
}
