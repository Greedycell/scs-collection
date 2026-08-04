const PiranhaMessage = require('../../PiranhaMessage')

class AvatarRankingListMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24403
    this.client = client
    this.version = 13
  }

  async encode () {
    //this.writeInt(-1) // entriescount (-1 = no entries)

    this.writeInt(1) // entriescount
    this.writeLong(0, 1) // AllianceId
    this.writeString('Boomers') // AllianceName
    this.writeInt(1) // Rank
    this.writeInt(1)//this.writeDataReference(1) // AllianceBadge
    this.writeInt(1) // Type
    this.writeInt(0)
    this.writeInt(100) // MemberCount
    this.writeInt(1) // Points

    this.writeInt(0)
    this.writeInt(50)  // MaximumMembers
  }
}

module.exports = AvatarRankingListMessage