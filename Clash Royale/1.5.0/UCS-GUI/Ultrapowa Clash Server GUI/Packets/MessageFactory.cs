namespace CRS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Packets.Messages.Client;

    #endregion

    internal class MessageFactory : IDisposable
    {
        /// <summary>
        ///     A list of all available <see cref="Message" /> s.
        /// </summary>
        internal static Dictionary<int, Type> m_vMessages;

        /// <summary>
        ///     Initialize a new instance of the <see cref="MessageFactory" />
        ///     class. When initialized, it will fill the list with all available
        ///     <see cref="Message" /> s.
        /// </summary>
        static MessageFactory()
        {
            m_vMessages = new Dictionary<int, Type>();

            m_vMessages.Add(10100, typeof(ClientHello));
            m_vMessages.Add(10101, typeof(Login));
            m_vMessages.Add(10107, typeof(ClientCapabilities));
            m_vMessages.Add(10108, typeof(KeepAlive));
            m_vMessages.Add(10113, typeof(SetDeviceToken));

            // m_vMessages.Add(10151, typeof(Google_Billing_Request));
            m_vMessages.Add(10212, typeof(ChangeAvatarName));

            // m_vMessages.Add(10159, typeof(Kunlub_Billing_Request));
            // m_vMessages.Add(10511, typeof(Apple_Billing_Request));
            m_vMessages.Add(10513, typeof(AskForPlayingFacebookFriends));

            m_vMessages.Add(10905, typeof(InboxOpened));

            //m_vMessages.Add(12903, typeof(RequestSectorState));
            m_vMessages.Add(12904, typeof(SectorCommand));

            // m_vMessages.Add(12906, typeof(Request_Spectator_State));
            m_vMessages.Add(12951, typeof(SendBattleEvent));

            // m_vMessages.Add(12952, typeof(Battle_Event_Spectate));
            m_vMessages.Add(14101, typeof(GoHome));
            m_vMessages.Add(14102, typeof(ExecuteCommandsMessage));
            
            m_vMessages.Add(14104, typeof(StartMission));
            //m_vMessages.Add(14105, typeof(HomeLogicStopped));
            m_vMessages.Add(14107, typeof(CancelMatchmake));
            //m_vMessages.Add(14111, typeof(Cancel_Challenge));
            m_vMessages.Add(14113, typeof(VisitHome));

            m_vMessages.Add(14201, typeof(BindFacebookAccount));
            m_vMessages.Add(14123, typeof(CancelChallengeMessage));

            // m_vMessages.Add(14212, typeof(Bind_Gamecenter_Account));
            m_vMessages.Add(14262, typeof(BindGoogleServiceAccount));

            m_vMessages.Add(14301, typeof(CreateAlliance));
            m_vMessages.Add(14302, typeof(AskForAllianceData));
            m_vMessages.Add(14303, typeof(AskForJoinableAlliancesList));
            m_vMessages.Add(14305, typeof(JoinAlliance));
            m_vMessages.Add(14307, typeof(KickAllianceMember));
            m_vMessages.Add(14324, typeof(SearchAlliances));
            m_vMessages.Add(14315, typeof(ChatToAllianceStream));
            m_vMessages.Add(14401, typeof(AskForAllianceRankingList));
            m_vMessages.Add(14402, typeof(AskForTVContent));
            m_vMessages.Add(14403, typeof(AskForAvatarRankingList));
            m_vMessages.Add(14404, typeof(AskForAvatarLocalRanking));
            m_vMessages.Add(14405, typeof(AskForAvatarStream));
            m_vMessages.Add(14406, typeof(AskForBattleReplayStream));
            m_vMessages.Add(14423, typeof(Cancel_Friendly_Battle));

            //m_vMessages.Add(14600, typeof(Name_Check_Request));
            m_vMessages.Add(16000, typeof(LogicDeviceLinkCodeStatus));
            m_vMessages.Add(16002, typeof(Link_Device));
            m_vMessages.Add(16103, typeof(Joinable_Tournaments));
            m_vMessages.Add(16113, typeof(Search_Tournaments));
        }

        public static object Read(Client c, Reader br, int packetType, int[] _Header)
        {
            if (m_vMessages.ContainsKey(packetType))
            {
                Debug.Write("Message " + packetType + ": " + m_vMessages[packetType].Name + " is handled");
                return Activator.CreateInstance(m_vMessages[packetType], c, br, _Header);
            }
            else
            {
                Debug.Write("Message " + packetType + " is unhandled");
                return null;
            }
        }

        /// <summary>
        ///     <see cref="Dispose" /> the class.
        /// </summary>
        public void Dispose()
        {
            m_vMessages.Clear();
        }
    }
}