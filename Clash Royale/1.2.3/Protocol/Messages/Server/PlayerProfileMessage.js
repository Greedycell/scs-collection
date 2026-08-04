const PiranhaMessage = require('../../PiranhaMessage')
const ClientAvatar = require('../../../Logic/ClientAvatar')
const cardUtils = require('../../../Utils/cardUtils')
const utils = require('../../../Utils')

class PlayerProfileMessage extends PiranhaMessage {
  constructor (client, HighID, LowID) {
    super()
    this.id = 24113
    this.client = client
    this.version = 1
    this.HighID = HighID
    this.LowID = LowID
  }

  async encode () {
    this.writeVInt(8)
    this.writeShort(255)
    this.writeVInt(1)

    // Deck
    {
      this.writeVInt(26000000)
      this.writeVInt(26000001)
      this.writeVInt(26000002)
      this.writeVInt(26000003)
      this.writeVInt(26000004)
      this.writeVInt(26000005)
      this.writeVInt(26000006)
      this.writeVInt(26000007)
    }

    this.writeLong(0, 1) // HighID, LowID
    this.writeVInt(0)
    this.writeVInt(0)
    this.writeVInt(1)

    const avatar = new ClientAvatar()
    avatar.encode(this)
  }
}

module.exports = PlayerProfileMessage