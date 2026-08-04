class ClientAvatar {
  async encode (self) {
    self.writeLogicLong(0, 1)
    self.writeLogicLong(0, 1)
    self.writeLogicLong(0, 1)

    self.writeString('Astral') // Name
    self.writeByte(0) // NameChanged

    self.writeVInt(1) // Arena
    self.writeVInt(0) // Trophies
    self.writeVInt(235)
    self.writeVInt(2380)
    self.writeVInt(0) // LegendaryTrophies
    self.writeVInt(0) // SeasonRecord
    self.writeVInt(0)
    self.writeVInt(0) // BestSeasonRank
    self.writeVInt(0) // BestSeasonTrophies

    self.writeByte(0)
    self.writeByte(37)

    self.writeVInt(0) // PreviousSeasonRank
    self.writeVInt(0) // PreviousSeasonTrophies
    self.writeVInt(0) // PreviousSeasonRecord

    self.writeVInt(0)
    self.writeByte(0)
    self.writeByte(8)

    self.writeVInt(16) // ComponentLength

    self.writeByte(5)
    self.writeByte(1)
    self.writeVInt(100) // Gold

    self.writeByte(5)
    self.writeByte(2)
    self.writeVInt(0) // WonChests

    self.writeByte(5)
    self.writeByte(3)
    self.writeVInt(0)

    self.writeByte(5)
    self.writeByte(4)
    self.writeVInt(0)

    self.writeByte(5)
    self.writeByte(5)
    self.writeVInt(100) // Gold

    self.writeByte(5)
    self.writeByte(12)
    self.writeVInt(419) // NextSuperMagical

    self.writeByte(5)
    self.writeByte(13)
    self.writeVInt(0)

    self.writeByte(5)
    self.writeByte(14)
    self.writeVInt(0) // DailyReward

    self.writeByte(5)
    self.writeByte(15)
    self.writeVInt(0) // NextLegendary

    self.writeByte(5)
    self.writeByte(16)
    self.writeVInt(1040) // ShopDays

    self.writeByte(5)
    self.writeByte(17)
    self.writeVInt(1044) // ShopLegendary

    self.writeByte(5)
    self.writeByte(18)
    self.writeVInt(1043) // ShopSM
    self.writeByte(5)
    self.writeByte(19)
    self.writeVInt(1049) // ShopArenaPack

    self.writeByte(5)
    self.writeByte(22)
    self.writeVInt(1042) // ShopEpic

    self.writeByte(5)
    self.writeByte(28)
    self.writeByte(0)

    self.writeByte(5)
    self.writeByte(29)
    self.writeByte(72000006) // LastGamemode

    self.writeByte(0x00)
    self.writeVInt(0) // Achievements
    self.writeVInt(0) // Achievements
    self.writeVInt(9)

    self.writeByte(5)
    self.writeByte(6)
    self.writeVInt(0) // HighestTrophies

    self.writeByte(5)
    self.writeByte(7)
    self.writeVInt(125) // ThreeCrownWins

    self.writeByte(5)
    self.writeByte(8)
    self.writeVInt(8) // UserCardsIdLength

    self.writeByte(5)
    self.writeByte(9)
    self.writeVInt(26000000) // FavouriteCard

    self.writeByte(5)
    self.writeByte(10)
    self.writeVInt(5000) // Donations

    self.writeByte(5)
    self.writeByte(11)
    self.writeVInt(10)

    self.writeByte(5)
    self.writeByte(20)
    self.writeVInt(6) // SurvivalMaxWins

    self.writeByte(5)
    self.writeByte(21)
    self.writeVInt(142)

    self.writeByte(5)
    self.writeByte(27)
    self.writeVInt(1) // MaxArena
    self.writeVInt(0)
    self.writeByte(0)
    self.writeByte(0)

    self.writeVInt(100) // Diamonds
    self.writeVInt(100) // FreeDiamonds
    self.writeVInt(0) // Experience
    self.writeVInt(1) // Level
    self.writeVInt(0)

    let InClan = 1 // 1 = No, 9 = Yes
    self.writeByte(InClan)
    if (InClan === 9){
      self.writeLogicLong(0, 1) // HighID, LowID
      self.writeString('Clashers') // Name
      self.writeVInt(1) // Badge
      self.writeByte(4) // Role
    }

    self.writeVInt(0) // BattlesPlayed
    self.writeVInt(0) // TournamentGamesPlayed
    self.writeVInt(0)
    self.writeVInt(0) // Wins
    self.writeVInt(0) // Losses

    self.writeHex('7d')
    self.writeVInt(8) // TrainerBattlesCompleted
    self.writeHex('010000000092b2d8b602')
    self.writeVInt(Date.now() / 1000 | 0)
    self.writeHex('a1ef1a')
  }
}

module.exports = ClientAvatar