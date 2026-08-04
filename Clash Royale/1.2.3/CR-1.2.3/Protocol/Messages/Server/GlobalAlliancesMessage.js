const PiranhaMessage = require('../../PiranhaMessage')

class GlobalAlliancesMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24401
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeVInt(1)
    this.writeVInt(1)
    this.writeVInt(1)
    this.writeString('GobelinLand') // AllianceName
    this.writeVInt(1)
    this.writeVInt(5000) // AllianceTrophies
    this.writeVInt(1)
    this.writeByte(16)
    this.writeVInt(26) // AllianceBadge
    this.writeVInt(0)
    this.writeVInt(5) // MembersCount
    this.writeVInt(5)
  }
}

module.exports = GlobalAlliancesMessage