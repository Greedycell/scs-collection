class ClientAvatar {
  async encode (self) {
    self.writeVInt(0)
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeLogicLong(0, 1) // HighID, LowID
    self.writeStringReference('Astral') // Name
    self.writeByte(1) // NameSet

    self.writeVInt(0)
    self.writeVInt(54)
    self.writeVInt(1) // Arena
    self.writeVInt(0) // Score

    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeVInt(6)
    self.writeVInt(5)

    self.writeVInt(5)
    self.writeVInt(1)
    self.writeVInt(1000000000) // Gold

    self.writeVInt(5)
    self.writeVInt(2)
    self.writeVInt(7)
    self.writeVInt(5)
    self.writeVInt(3)
    self.writeVInt(10)
    self.writeVInt(5)
    self.writeVInt(4)
    self.writeVInt(0)
    self.writeVInt(5)
    self.writeVInt(5)
    self.writeVInt(500000)
    self.writeVInt(0)
    self.writeVInt(7)
    self.writeVInt(60)
    self.writeVInt(7)
    self.writeVInt(9)
    self.writeVInt(60)
    self.writeVInt(8)
    self.writeVInt(9)
    self.writeVInt(60)
    self.writeVInt(9)
    self.writeVInt(9)
    self.writeVInt(60)
    self.writeVInt(4)
    self.writeVInt(1)
    self.writeVInt(60)
    self.writeVInt(5)
    self.writeVInt(1)
    self.writeVInt(60)
    self.writeVInt(6)
    self.writeVInt(1)
    self.writeVInt(60)
    self.writeVInt(10)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(60)
    self.writeVInt(10)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(5)
    self.writeVInt(8)
    self.writeVInt(9)
    self.writeVInt(7)
    self.writeVInt(26)
    self.writeVInt(0)
    self.writeVInt(11)
    self.writeVInt(26)
    self.writeVInt(1)
    self.writeVInt(8)
    self.writeVInt(26)
    self.writeVInt(3)
    self.writeVInt(9)
    self.writeVInt(26)
    self.writeVInt(13)
    self.writeVInt(13)
    self.writeVInt(26)
    self.writeVInt(14)
    self.writeVInt(6)
    self.writeVInt(28)
    self.writeVInt(0)
    self.writeVInt(2)
    self.writeVInt(26)
    self.writeVInt(12)
    self.writeVInt(4)

    self.writeVInt(1000000000) // Diamonds
    self.writeVInt(1000000000) // FreeDiamonds
    self.writeVInt(0) // Experience
    self.writeVInt(12) // Level
    self.writeVInt(0)

    // 7 = Name already set + no clan
    // 8 = Set name popup + clan
    // 9 = Name already set + clan
    // < 7 =  Set name popup
    const InClan = 0
    if (InClan == 1) {
      self.writeBoolean(true)
      self.writeByte(9)
      self.writeByte(0)
      self.writeString('Clashers') // Name
      self.writeVInt(16)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(1)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(8)
      self.writeBoolean(true)
      self.writeVInt(-20)
    } else {
      self.writeByte(0)
      self.writeByte(0) // NameSet
    
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(6)
      self.writeVInt(0)
      self.writeBoolean(true)
      self.writeVInt(-131170575)
    }
  }
}

module.exports = ClientAvatar