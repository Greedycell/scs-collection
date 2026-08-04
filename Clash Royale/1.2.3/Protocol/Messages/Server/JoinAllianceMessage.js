const PiranhaMessage = require('../../PiranhaMessage')

class JoinAllianceMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24111
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeByte(145)
    this.writeByte(3)
    this.writeLong(0, 1) // AllianceHighID, AllianceLowID
    this.writeString('Clashers') // AllianceName
    this.writeByte(16)
    this.writeVInt(0) // AllianceBadge
    this.writeHex('0101047F7F0000')
  }
}

module.exports = JoinAllianceMessage