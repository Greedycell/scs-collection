using System;
using ClashofClans.Logic;
using ClashofClans.Utilities.Utils;

namespace ClashofClans.Protocol.Messages.Server.Battle
{
    public class Village2AttackAvatarDataMessage : PiranhaMessage
    {
        public Village2AttackAvatarDataMessage(Device device) : base(device)
        {
            Id = 25863;
            device.CurrentState = Device.State.Battle;
            Device.LastVisitHome = DateTime.UtcNow;
        }

        public Player Enemy { get; set; }

        public override void Encode()
        {
            Enemy.Home.Tick();

            //Device.Player.LogicClientAvatar(Writer);
            Enemy.LogicClientAvatar(Writer);
            Enemy.LogicClientHome(Writer);

            Writer.WriteLong(Enemy.Home.Id);
            Writer.WriteInt(-1); // Timestamp
        }
    }
}