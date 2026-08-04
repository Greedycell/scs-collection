using System;
using System.Collections.Generic;
using ClashofClans.Protocol.Messages.Client;
using ClashofClans.Protocol.Messages.Client.Alliance;
using ClashofClans.Protocol.Messages.Client.Home;
using ClashofClans.Protocol.Messages.Client.Login;
using ClashofClans.Protocol.Messages.Client.Scoring;

namespace ClashofClans.Protocol
{
    public class LogicMagicMessageFactory
    {
        public static Dictionary<int, Type> Messages;

        static LogicMagicMessageFactory()
        {
            Messages = new Dictionary<int, Type>
            {
                {10100, typeof(ClientHelloMessage)},
                {10101, typeof(LoginMessage)},
                {12031, typeof(KeepAliveMessage)},
                //{10113, typeof(SetDeviceTokenMessage)},
                {10601, typeof(SendGlobalChatLineMessage)},
                /*{10905, typeof(NewsSeenMessage)},*/
                {10936, typeof(GoHomeMessage)},
                {13803, typeof(GoHomeMessage)},
                {11186, typeof(AskForAllianceDataMessage)},
                {11734, typeof(AskForAvatarProfileMessage)},
                {12461, typeof(AskForLastAvatarTournamentResultsMessage)},
                {12865, typeof(AskForAllianceRankingListMessage)},
                {12906, typeof(EndClientTurnMessage)},
                {13586, typeof(AskForLeagueMemberListMessage)},
                {13723, typeof(AskForAvatarLocalRankingListMessage)},
                {14322, typeof(VisitHomeMessage)},
                {14359, typeof(AskForAvatarRankingListMessage)},
                {14466, typeof(ChatToAllianceStreamMessage)},
                {15718, typeof(AttackNpcMessage)},
                {16203, typeof(DebugEventMessage)},
                {17173, typeof(ChangeAvatarNameMessage)},
            };
        }
    }
}