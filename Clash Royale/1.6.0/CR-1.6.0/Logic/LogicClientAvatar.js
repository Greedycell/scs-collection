class LogicClientAvatar {
  async encode (self) {
    self.writeLogicLong(0, 1) // Id
    self.writeLogicLong(0, 1) // AccountId
    self.writeLogicLong(0, 1) // HomeId
    self.writeStringReference('Astral') // Name
    self.writeVInt(0) // NameChangeState
    self.writeDataReference(54, 1) // Arena
    self.writeVInt(0) // Score
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(7) // Resources
    {
      // Gold
      self.writeVInt(1)
      self.writeDataReference(5, 1) // ClassId, InstanceId
      self.writeVInt(100) // Amount

      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
    }
    self.writeVInt(100) // Diamonds
    self.writeVInt(100) // FreeDiamonds
    self.writeVInt(0) // ExpPoints
    self.writeVInt(1) // ExpLevel
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
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
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(9)
    self.writeVInt(0)
    self.writeBoolean(false)
  }
}

module.exports = LogicClientAvatar