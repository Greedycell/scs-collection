class ClientAvatar {
  async encode (self) {
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID

    self.writeString('') // Name
    self.writeVInt(0) // NameChanges
    self.writeVInt(1) // Arena
    self.writeVInt(0) // Trophies

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0) // LegendaryTrophies

    self.writeVInt(0) // CurrentSeasonTrophies
    self.writeVInt(0)
    self.writeVInt(0) // DisplaysNearLeague // maybe never used

    self.writeVInt(0) // BestSeasonTrophies
    self.writeVInt(0) // Rank
    self.writeVInt(0) // Trophies

    // League
    self.writeVInt(0) // CurrentTrophies
    self.writeVInt(0) // PastTrophies
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(0) // set this 1 and it appears on the profile

    self.writeVInt(8)

    // Game Variables
    self.writeVInt(10)
    self.writeVInt(5)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(5)
    self.writeVInt(1)
    self.writeVInt(1000000000) // Gold

    self.writeVInt(5)
    self.writeVInt(3)
    self.writeVInt(2)

    self.writeVInt(5) // NewCrowns
    self.writeVInt(4)
    self.writeVInt(0)

    self.writeVInt(5)
    self.writeVInt(5)
    self.writeVInt(1000000000) // Gold

    self.writeVInt(5)
    self.writeVInt(13)
    self.writeVInt(0) // NewGold

    self.writeVInt(5)
    self.writeVInt(14)
    self.writeVInt(0)

    self.writeVInt(5)
    self.writeVInt(16)
    self.writeVInt(51)

    self.writeVInt(5)
    self.writeVInt(28)
    self.writeVInt(0)

    self.writeVInt(5)
    self.writeVInt(29)
    self.writeVInt(72000006)

    self.writeVInt(0) // CompletedAchievements

    // Achievements
    self.writeVInt(0) // AchievementCount
    self.writeVInt(0) // AchievementCount

    // Profile Statistics
    self.writeVInt(6)
    self.writeVInt(5)
    self.writeVInt(6)
    self.writeVInt(30)

    self.writeVInt(5)
    self.writeVInt(7)
    self.writeVInt(0) // ThreeCrownWinCount

    self.writeVInt(5)
    self.writeVInt(8)
    self.writeVInt(5) // CardsFound

    self.writeVInt(5)
    self.writeVInt(1) // Count
    self.writeVInt(26000048) // CardID

    self.writeVInt(5)
    self.writeVInt(11)
    self.writeVInt(32)

    self.writeVInt(5)
    self.writeVInt(27)
    self.writeVInt(1)

    self.writeVInt(0)
    self.writeVInt(0) // NPC? / Count?
    self.writeVInt(0)

    self.writeVInt(1000000000) // Diamonds
    self.writeVInt(1000000000) // FreeDiamonds

    self.writeVInt(0) // Experience
    self.writeVInt(13) // Level

    self.writeVInt(0) // AvatarUserLevelTier

    self.writeVInt(0) // HasAlliance

    // Battle Statistics
    self.writeVInt(0) // GamesPlayed
    self.writeVInt(0) // TournamentMatchesPlayed
    self.writeVInt(0)
    self.writeVInt(0) // Wins
    self.writeVInt(0) // Losses

    self.writeVInt(0)

    self.writeVInt(7)

    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0) // Has Challenge
    //  self.writeVInt() // ID
    //  self.writeVInt(0) // WINS
    //  self.writeVInt(0) // LOSSES

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0) // AccountCreated
    self.writeVInt(0) // PlayTime
  }
}

module.exports = ClientAvatar