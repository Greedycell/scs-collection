const PiranhaMessage = require('../../PiranhaMessage')

class EndClientTurnMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14102
    this.version = 1
  }

  async decode () {}

  async process () {}
}

module.exports = EndClientTurnMessage
