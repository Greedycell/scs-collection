namespace CRS.Core.Network.UDP
{
    #region Usings

    using System.Net;

    #endregion Usings

    public struct Received
    {
        public IPEndPoint Sender;
        public byte[] Message;
    }
}