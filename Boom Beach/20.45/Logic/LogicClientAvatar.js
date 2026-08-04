const LogicBase = require('./LogicBase')
const LogicPlayerMap = require('./LogicPlayerMap')

class LogicClientAvatar {
  async encode (self) {
    new LogicBase().encode(self)
    self.writeLong(0, 1) // Id
    self.writeLong(0, 1) // CurrentId
    let InAlliance = true
    self.writeBoolean(InAlliance) // InAlliance
    if (InAlliance === true) {
      self.writeLong(0, 1) // AllianceId
      self.writeString('Boomers') // AllianceName
      self.writeInt(1) // AllianceBadge
      self.writeInt(2) // AllianceRole (1 = Member, 2 = Leader, 3 = Elder, 4 = Co-Leader)
    }
    self.writeInt(1) // TownHallLevel
    self.writeString('Astral') // Name
    self.writeString(null) // FacebookId
    self.writeInt(50) // ExpLevel
    self.writeInt(0) // ExpPoints
    self.writeInt(1000000000) // Diamonds
    self.writeInt(0) // FreeDiamonds
    self.writeInt(0) // AttackRating
    self.writeInt(0) // AttackKFactor
    self.writeInt(10000) // Score
    self.writeBoolean(false) // NameSetByUser 
    self.writeInt(0) // CumulativePurchasedDiamonds
    self.writeInt(0)
    
    // LogicDataSlotArrays
    self.writeInt(0)
    self.writeInt(4) // ResourceSlotData
    {
      // Gold
      self.writeInt(3000001)
      self.writeInt(1000000000)
      // Wood
      self.writeInt(3000002)
      self.writeInt(1000000000)
      // Stone
      self.writeInt(3000003)
      self.writeInt(1000000000)
      // Iron
      self.writeInt(3000004)
      self.writeInt(1000000000)
    }
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)

    //new LogicPlayerMap().encode(self)
    // temporary snippet
    self.writeInt(0)
    self.writeInt(0)
    self.writeBoolean(false)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeBoolean(false)
  }
}

module.exports = LogicClientAvatar