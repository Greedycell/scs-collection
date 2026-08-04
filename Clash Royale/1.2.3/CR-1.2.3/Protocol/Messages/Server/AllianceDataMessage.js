const PiranhaMessage = require('../../PiranhaMessage')

class AllianceDataMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24301
    this.client = client
    this.version = 1
  }

  async encode () {
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

    this.writeByte(1) // MembersCount
    {
      this.writeInt(1)
      this.writeHex('0000DE74000000103135363536373732')
      // AllianceMemberEntry
      {
        this.writeLong(0, 1) // HighID, LowiD
        this.writeString('Astral') // Name
        this.writeByte(54)
        this.writeByte(8)
        this.writeVInt(2) // Role (1 = Member, 2 = Leader, 3 = Elder, 4 = Co-Leader)
        this.writeVInt(12) // Level
        this.writeVInt(3000) // Score
        this.writeByte(0)
      }
      this.writeInt(0)
      this.writeByte(0)
      this.writeByte(0)
    }
  }
}

module.exports = AllianceDataMessage