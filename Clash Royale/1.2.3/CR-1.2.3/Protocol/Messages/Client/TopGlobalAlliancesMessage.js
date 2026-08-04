const PiranhaMessage = require('../../PiranhaMessage')
const GlobalAlliancesMessage = require('../Server/GlobalAlliancesMessage')

class TopGlobalAlliancesMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14401
    this.version = 1
  }

  async decode () {}

  async process () {
    await new GlobalAlliancesMessage(this.client).send()
  }
}

module.exports = TopGlobalAlliancesMessage