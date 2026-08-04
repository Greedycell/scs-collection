class LogicLeaveAllianceCommand {
  constructor() {}

  async decode () {}

  async encode (self) {
    self.writeByte(144)
    self.writeByte(3)
    self.writeLong(0, 1) // AllianceHighID, AllianceLowID
    self.writeString('Clashers') // AllianceName
    self.writeHex('10B10200087F7F0000')
  }
}

module.exports = LogicLeaveAllianceCommand