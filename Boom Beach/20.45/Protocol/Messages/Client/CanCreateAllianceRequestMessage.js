const PiranhaMessage = require('../../PiranhaMessage')
const CanCreateAllianceResponseMessage = require('../Server/CanCreateAllianceResponseMessage')

class CanCreateAllianceRequestMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14358
    this.version = 9
  }

  async decode () {}

  async process () {
    await new CanCreateAllianceResponseMessage(this.client).send()
  }
}

module.exports = CanCreateAllianceRequestMessage