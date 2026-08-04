class LogicJoinAllianceCommand {
  constructor() {}

  async encode (self) {
    self.writeLong(0, 1) // AllianceId
    self.writeInt(0)
  }
}

module.exports = LogicJoinAllianceCommand