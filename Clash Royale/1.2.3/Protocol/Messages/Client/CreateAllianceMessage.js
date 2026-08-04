const PiranhaMessage = require('../../PiranhaMessage')
const JoinAllianceMessage = require('../Server/JoinAllianceMessage')
const AllianceStreamMessage = require('../Server/AllianceStreamMessage')

class CreateAllianceMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14301
    this.version = 1
  }

  async decode () {}

  async process () {
    await new JoinAllianceMessage(this.client).send()
    await new AllianceStreamMessage(this.client).send()
  }
}

module.exports = CreateAllianceMessage