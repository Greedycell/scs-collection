const PiranhaMessage = require('../../PiranhaMessage')
const OwnHomeDataMessage = require('../Server/OwnHomeDataMessage')

class VisitNpcMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14135
    this.version = 10
  }

  async decode () {}

  async process () {
    await new OwnHomeDataMessage(this.client).send()
  }
}

module.exports = VisitNpcMessage