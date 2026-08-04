const PiranhaMessage = require('../../PiranhaMessage')
const LocalPlayersMessage = require('../Server/LocalPlayersMessage')

class TopLocalPlayersMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14404
    this.version = 1
  }

  async decode () {}

  async process () {
    await new LocalPlayersMessage(this.client).send()
  }
}

module.exports = TopLocalPlayersMessage