class LogicClientAvatar {
  async encode (self) {
    self.writeLogicLong(0, 1) // Id
    self.writeLogicLong(0, 1) // AccountId
    self.writeLogicLong(0, 1) // HomeId
    self.writeStringReference('Astral') // Name
    self.writeBoolean(true) // NameSetByUser
    self.writeVInt(0) // NameChangeState
    self.writeDataReference(54, 1) // Arena
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
    {
      self.writeVInt(5)

      self.writeVInt(5)
      self.writeVInt(1)
      self.writeVInt(100) // Gold

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
    }

    self.writeVInt(100) // Diamonds
    self.writeVInt(100) // FreeDiamonds
    self.writeVInt(0) // ExpPoints
    self.writeVInt(1) // ExpLevel
    self.writeVInt(0)
    if (false) {
      self.writeBoolean(true) // IsInAlliance
      self.writeLogicLong(0, 1) // AllianceId
      self.writeStringReference('Clashers') // AllianceName
      self.writeDataReference(0, 0) // AllianceBadge
      self.writeVInt(2) // AllianceRole
    } else {
      self.writeBoolean(false) // IsInAlliance
    }
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(9)
    self.writeVInt(9)
    self.writeVInt(9)
    self.writeVInt(0)
    self.writeBoolean(false)
  }
}

module.exports = LogicClientAvatar