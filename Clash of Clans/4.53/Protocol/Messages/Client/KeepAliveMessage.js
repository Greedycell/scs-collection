const PiranhaMessage = require('../../PiranhaMessage')
const KeepAliveServerMessage = require('../Server/KeepAliveServerMessage')

class KeepAliveMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 10108
    this.version = 1
  }

  async decode () {}

  async process () {
    await new KeepAliveServerMessage(this.client).send()
  }
}

module.exports = KeepAliveMessage