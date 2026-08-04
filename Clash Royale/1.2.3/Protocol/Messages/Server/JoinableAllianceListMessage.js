const PiranhaMessage = require('../../PiranhaMessage')

class JoinableAllianceListMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24304
    this.client = client
    this.version = 1
  }

  async encode () {
    let num = 0

    this.writeVInt(num + 1)

    // EncodeJoinableAlliance
    {
      this.writeLong(0, 1) // AllianceHighID, AllianceLowID
      this.writeString('Clashers') // AllianceName
      this.writeByte(16)
      this.writeVInt(0) // AllianceBadge
      this.writeVInt(1) // Type
      this.writeVInt(1) // MembersCount
      this.writeVInt(3000) // Score
      this.writeVInt(0) // RequiredScore
      this.writeByte(0)
      this.writeHex('00008001')
      this.writeByte(0)
      this.writeHex('94250101399701')
    }
  }
}

module.exports = JoinableAllianceListMessage