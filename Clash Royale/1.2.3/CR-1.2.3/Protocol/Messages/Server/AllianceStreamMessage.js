const PiranhaMessage = require('../../PiranhaMessage')

class AllianceStreamMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24311
    this.client = client
    this.version = 1
  }

  async encode () {
    let num = 1
    this.writeVInt(1)

    this.writeVInt(num)
    // StreamEntry::encode
    {
      this.writeLogicLong(0, 1) // AllianceHighID, AllianceLowID
      this.writeLogicLong(0, 1) // AllianceHighID, AllianceLowID
      this.writeLogicLong(0, 1) // AllianceHighID, AllianceLowID
      this.writeString('Clashers')
      this.writeVInt(8)
      this.writeVInt(2)
      this.writeVInt(30)
      this.writeBoolean(false)
      this.writeInt(0)
      this.writeInt(0)
      this.writeVInt(0)
      this.writeBoolean(false)
      this.writeVInt(0)
    }
  }
}

module.exports = AllianceStreamMessage