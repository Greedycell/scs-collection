const PiranhaMessage = require('../../PiranhaMessage')

class AllianceStreamMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24312
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeByte(2)
    this.writeHex('0390EDBAEB01')

    // StreamEntry::encode
    {
      this.writeLong(0, 1) // HighID, LowID
      this.writeLong(0, 1) // HighID, LowID
      this.writeString('Astral') // Name
      this.writeByte(8)
      this.writeByte(2) // Role (1 = Member, 2 = Leader, 3 = Elder, 4 = Co-Leader)
      this.writeByte(30)
      this.writeByte(0)
    }
  }
}

module.exports = AllianceStreamMessage