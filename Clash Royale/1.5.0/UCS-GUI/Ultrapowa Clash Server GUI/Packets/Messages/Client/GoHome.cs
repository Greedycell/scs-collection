namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class GoHome : Message
    {
        public const ushort PacketID = 14101;

        /// <summary>
        ///     Initialize a new instance of the <see cref="GoHome" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public GoHome(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Go_Home.
            this.Decrypt2();
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                var test = this.Reader.ReadVInt();
                Debug.Write("test: " + test);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            //if (level.GetPlayerAvatar().State == 2)
            //{
            //    var info = default(ClientAvatar.AttackInfo);
            //    if (!level.GetPlayerAvatar().AttackingInfo.TryGetValue(level.GetPlayerAvatar().GetId(), out info))
            //    {
            //        Logger.WriteLine("Unable to obtain attack info.");
            //        Debug.Write("Unable to obtain attack info.");
            //    }
            //    else
            //    {
            //        var defender = info.Defender;
            //        var attacker = info.Attacker;

            //        var lost = info.Lost;
            //        var reward = info.Reward;

            //        int attackerscore = attacker.GetPlayerAvatar().GetScore();
            //        int defenderscore = defender.GetPlayerAvatar().GetScore();

            //        if (defender.GetPlayerAvatar().GetScore() > 0)
            //            defender.GetPlayerAvatar().SetScore(defenderscore -= lost);

            //        attacker.GetPlayerAvatar().SetScore(attackerscore += reward);
            //        //attacker.GetPlayerAvatar().GetUnits().Clear(); //Remove all fake troop //Will be remove once training fixed
            //        //attacker.GetPlayerAvatar().GetSpells().Clear(); //Remove all fake spell //Will be remove once training fixed
            //        attacker.GetPlayerAvatar().AttackingInfo.Clear(); //Since we use userid for now,We need to clear to prevent overlapping
            //        Resources(attacker);

            //        DatabaseManager.Singelton.Save(attacker);
            //        DatabaseManager.Singelton.Save(defender);

            //    }
            //    level.GetPlayerAvatar().State = 0; //We are home (NPC Attck will not trigger above funtion yet)
            //}


            //level.Tick();
            //var alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetClanID());
            //new OwnHomeData(this.Client, level).Send();
            //if (alliance != null)
            //{
            //    //new AllianceStreamMessage(Client, alliance).Send();
            //}
            PacketManager.Send(new OwnHomeData(this.Client, level));
        }
        //public void Resources(Level level)
        //{
        //    var avatar = level.GetPlayerAvatar();
        //    var currentGold = avatar.GetResourceCount(ObjectManager.DataTables.GetResourceByName("Gold"));
        //    var goldLocation = ObjectManager.DataTables.GetResourceByName("Gold");

        //    if (currentGold >= 1000000000)
        //    {
        //        avatar.SetResourceCount(goldLocation, currentGold + 10);
        //    }
        //    else if (currentGold <= 999999999)
        //    {
        //        avatar.SetResourceCount(goldLocation, currentGold + 1000);
        //    }
        //}
    }
}