const PiranhaMessage = require('../../PiranhaMessage')
const AllianceListMessage = require('../Server/AllianceListMessage')

class SearchAlliancesMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14324
    this.version = 11
  }

  async decode () {}

  async process () {
    await new AllianceListMessage(this.client).send()
  }
}

module.exports = SearchAlliancesMessage