const PiranhaMessage = require('../../PiranhaMessage')
const GlobalPlayersMessage = require('../Server/GlobalPlayersMessage')

class TopGlobalPlayersMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14403
    this.version = 1
  }

  async decode () {}

  async process () {
    await new GlobalPlayersMessage(this.client).send()
  }
}

module.exports = TopGlobalPlayersMessage