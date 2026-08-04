class ClientAvatar {
  async encode (self) {
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID

    self.writeString('') // Name
    self.writeVInt(0) // Name Changes
    self.writeVInt(1) // Arena
    self.writeVInt(0) // Trophies

    self.writeVInt(0) // Unknown

    self.writeVInt(0) // Legend Trophies
    self.writeVInt(0) // Current Trophies => Season Higheset

    self.writeVInt(0) // Unknown

    self.writeVInt(0) // Leaderboard NR => Best Season
    self.writeVInt(0) // Trophies => Best Season
    self.writeVInt(0) // Unknown
    self.writeVInt(30) // Unknown
    self.writeVInt(0) // Leaderboard NR => Previous Season
    self.writeVInt(0) // Trophies => Previous Season
    self.writeVInt(0) // Highest Trophies

    self.writeVInt(0) // Unknown
    self.writeVInt(0) // Unknown

    self.writeVInt(0) // Resources
    self.writeVInt(2) // Resources
    {
      self.writeVInt(5) // Type
      self.writeVInt(1) // ID
      self.writeVInt(100) // Gold Amount
      self.writeVInt(5) // Type
      self.writeVInt(5) // ID
      self.writeVInt(100) // Gold Amount
    }

    self.writeByte(0)

    self.writeVInt(0) // Achievements
    self.writeVInt(0) // Achievements Completed

    self.writeVInt(0)
    self.writeByte(0)
    self.writeByte(0)

    self.writeVInt(100) // Diamonds
    self.writeVInt(100) // FreeDiamonds
    self.writeVInt(0) // Experience
    self.writeVInt(1) // Level
    self.writeByte(0) // NameSet

    // 7 = Name already set + no clan
    // 8 = Set name popup + clan
    // 9 = Name already set + clan
    // < 7 =  Set name popup
    const InClan = 0
    if (InClan == 1) {
      self.writeByte(9)
      self.writeLogicLong(0, 1)
      self.writeString('Greedycell') // Name
      self.writeByte(1) // Badge
      self.writeVInt(4) // Role
    }
    else {
      self.writeByte(0) 
    }

    self.writeVInt(0) // Games Played
    self.writeVInt(0) // Matched Played -> Tournament Stats
    self.writeVInt(0) // Unknown
    self.writeVInt(0) // Win
    self.writeVInt(0) // Loses
    self.writeVInt(0) // Win Streak
    self.writeVInt(7) // Tutorial
    self.writeVInt(0) // Tournament? (bool)

    self.writeVInt(0) // Unknown
    self.writeVInt(0) // Unknown
    self.writeVInt(0) // Countdown?
    self.writeVInt(0) // Creation Date
    self.writeVInt(0) // Time Played
  }
}

module.exports = ClientAvatar