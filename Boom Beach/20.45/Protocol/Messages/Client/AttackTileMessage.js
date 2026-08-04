const PiranhaMessage = require('../../PiranhaMessage')
const NpcDataMessage = require('../Server/NpcDataMessage')

class AttackTileMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14165
    this.version = 9
  }

  async decode () {}

  async process () {
    await new NpcDataMessage(this.client).send()
  }
}

module.exports = AttackTileMessage