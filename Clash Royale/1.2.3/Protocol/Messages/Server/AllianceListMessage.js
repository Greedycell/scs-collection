const PiranhaMessage = require('../../PiranhaMessage')

class AllianceListMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24310
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeString('Clashers') // SearchString

    this.writeVInt(1) // Count
    // EncodeFullEntry
    {
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

      this.writeString('Test description!') // AllianceDescription
    }
  }
}

module.exports = AllianceListMessage