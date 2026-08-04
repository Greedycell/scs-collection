using System;
using System.Linq;
using ClashofClans.Logic;
using ClashofClans.Logic.Manager;
using ClashofClans.Logic.Manager.Items;
using ClashofClans.Protocol.Messages.Server;
using ClashofClans.Utilities.Netty;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Messages.Client
{
    public class SendGlobalChatLineMessage : PiranhaMessage
    {
        public SendGlobalChatLineMessage(Device device, IByteBuffer buffer) : base(device, buffer)
        {
            RequiredState = Device.State.NotDefinied;
        }

        private string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadScString();
        }

        public override async void Process()
        {
            if ((DateTime.UtcNow - Device.LastChatMessage).TotalSeconds >= 1.0)
            {
                if (!string.IsNullOrEmpty(Message))
                {
                    Resources.ChatManager.Process(new GlobalChatEntry
                    {
                        Message = Message,
                        SenderName = Device.Player.Home.Name,
                        SenderId = Device.Player.Home.Id,
                        SenderExpLevel = Device.Player.Home.ExpLevel,
                        SenderLeague = Device.Player.Home.League
                    });

                    Device.LastChatMessage = DateTime.UtcNow;
                }
            }
        }
    }
}