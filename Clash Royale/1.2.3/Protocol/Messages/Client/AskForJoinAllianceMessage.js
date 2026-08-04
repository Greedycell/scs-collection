const PiranhaMessage = require('../../PiranhaMessage')
const AllianceStreamMessage = require('../Server/AllianceStreamMessage')
const JoinAllianceMessage = require('../Server/JoinAllianceMessage')

class AskForJoinAllianceMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14305
    this.version = 1
  }

  async decode () {}

  async process () {
    await new AllianceStreamMessage(this.client).send()
    await new JoinAllianceMessage(this.client).send()
  }
}

module.exports = AskForJoinAllianceMessage