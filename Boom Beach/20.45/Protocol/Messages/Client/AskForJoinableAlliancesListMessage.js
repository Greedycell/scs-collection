const PiranhaMessage = require('../../PiranhaMessage')
const JoinableAllianceListMessage = require('../Server/JoinableAllianceListMessage')

class AskForJoinableAlliancesListMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14303
    this.version = 11
  }

  async decode () {}

  async process () {
    await new JoinableAllianceListMessage(this.client).send()
  }
}

module.exports = AskForJoinableAlliancesListMessage