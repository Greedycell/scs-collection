const PiranhaMessage = require('../../PiranhaMessage')

class AllianceDataMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24301
    this.client = client
    this.version = 11
  }

  async encode () {
    // AllianceFullEntry::encode
    {
      // AllianceHeaderEntry::encode
      {
        this.writeLong(0, 1) // AllianceId
        this.writeString('Boomers') // AllianceName
        this.writeInt(1) // AllianceBadge //ByteStreamHelper::writeDataReference((int)a2, *(this + 2));
        this.writeInt(1) // Type
        this.writeInt(1) // MemberCount
        this.writeInt(0) // Score
        this.writeInt(0) // RequiredScore
        this.writeString('test_tag_1234')
        this.writeInt(50) // MaximumMembers
        this.writeInt(0)
        this.writeInt(0)
      }

      this.writeString('Test description') // AllianceDescription
      this.writeInt(0)
      this.writeInt(0) // Intel
      this.writeInt(1) // MemberCount

      // AllianceMemberEntry::encode
      {
        this.writeLong(0, 1) // Id
        this.writeString(null) // FacebookId
        this.writeString('Astral') // Name
        this.writeInt(2) // Role (1 = Member, 2 = Leader, 3 = Elder, 4 = Co-Leader)
        this.writeInt(1) // ExpLevel
        this.writeInt(0)
        this.writeInt(0)
        this.writeInt(0)
        this.writeBoolean(false)
        this.writeInt(0)
        this.writeInt(0)
        this.writeBoolean(true)
        this.writeLong(0, 1) // Id
      }
    }
  }
}

module.exports = AllianceDataMessage