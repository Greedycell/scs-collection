const PiranhaMessage = require('../../PiranhaMessage')
const RoyalTvContentMessage = require('../Server/RoyalTvContentMessage')

class AskForTvContentMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14402
    this.version = 1
  }

  async decode () {}

  async process () {
    await new RoyalTvContentMessage(this.client).send()
  }
}

module.exports = AskForTvContentMessage