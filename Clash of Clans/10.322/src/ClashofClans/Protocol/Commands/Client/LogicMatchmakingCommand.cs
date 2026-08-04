using ClashofClans.Logic;
using ClashofClans.Protocol.Messages.Server.Battle;
using DotNetty.Buffers;

namespace ClashofClans.Protocol.Commands.Client
{
    public class LogicMatchmakingCommand : LogicCommand
    {
        public LogicMatchmakingCommand(Device device, IByteBuffer buffer) : base(device, buffer)
        {
        }

        public override async void Process()
        {
            Device.CurrentState = Device.State.Battle;
            Device.CurrentBattleType = Device.BattleType.Multiplayer;

            Device.Player.Home.GameMatchmakingManager.Init(Device);
        }
    }
}