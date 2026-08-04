const PiranhaMessage = require('../../PiranhaMessage')

class AllianceListMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24310
    this.client = client
    this.version = 11
  }

  async encode () {
    //this.writeInt(-1) // entriescount (-1 = no entries)

    this.writeInt(1) // entriescount
    this.writeLong(0, 1) // AllianceId
    this.writeString('Boomers') // AllianceName
    this.writeInt(1)//this.writeDataReference(1) // AllianceBadge
    this.writeInt(0)
    this.writeInt(0)
    this.writeInt(0)
    this.writeInt(1) // MemberCount

    this.writeInt(0)
    this.writeInt(50)  // MaximumMembers
  }
}

module.exports = AllianceListMessage