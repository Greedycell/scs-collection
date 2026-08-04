namespace CRS.Core.Network
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Net;

    using Extensions;

    using Packets;

    using Extensions.Binary;

    using Logic;

    #endregion

    /// <summary>
    /// This class is used to process outgoing and incoming messages.
    /// </summary>
    internal static class Processor
    {
        /// <summary>
        /// A List of Messages, containing all incoming
        /// <see cref="CRS.Packets.Message" /> .
        /// </summary>
        private static Queue<Message> Incoming = new Queue<Message>();

        /// <summary>
        /// A List of Messages, containing all outgoing
        /// <see cref="CRS.Packets.Message" /> .
        /// </summary>
        private static Queue<Message> Outgoing = new Queue<Message>();

        /// <summary>
        /// Initialize a new instance of the <see cref="Processor" /> class.
        /// </summary>
        static Processor()
        {
            Outgoing = new Queue<Message>();
            Incoming = new Queue<Message>();
        }

        public static Command Handle(this Command _Command)
        {
            _Command.Encode();
            _Command.Process();

            return _Command;
        }

        /// <summary>
        /// Decrypt, Decode, then Process the specified
        /// <see cref="CRS.Packets.Message" /> .
        /// </summary>
        /// <param name="_Message">
        /// The <see cref="CRS.Packets.Message" /> which need to be processed.
        /// </param>
        public static void Recept(this Message _Message)
        {
            Level pl = _Message.Client.GetLevel();
            _Message.Decrypt();
            _Message.Decode();
            _Message.Process(pl);
        }

        /// <summary>
        /// Encrypt, Encode, then <see cref="Processor.Send" /> the specified
        /// <see cref="CRS.Packets.Message" /> .
        /// </summary>
        /// <param name="_Message">
        /// The <see cref="CRS.Packets.Message" /> which need to be processed.
        /// </param>
        public static void Send(this Message _Message)
        {
            Level pl = _Message.Client.GetLevel();
            _Message.Encode();
            _Message.Process(pl);
            _Message.Encrypt();

            _Message.Client.Connection.BeginSend(
                _Message.GetPacket(),
                0,
                _Message.GetPacket().Length,
                0,
                SendCallback,
                _Message);
        }

        private static void SendCallback(IAsyncResult _Ar)
        {
            Message _Message = _Ar.AsyncState as Message;

            if (Settings.Settings.Debug)
            {
                Console.WriteLine("SERVER                  -> " + ConsolePad.Padding(_Message.GetType().Name) + " -> "
                                  + ConsolePad.Padding(
                                                       ((IPEndPoint)_Message.Client.Connection.RemoteEndPoint).Address
                                                                                                              .ToString())
                                  + " -> " + _Message.Client.Interface);
            }
        }
    }
}