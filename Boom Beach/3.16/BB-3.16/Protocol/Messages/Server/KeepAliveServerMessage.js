const PiranhaMessage = require('../../PiranhaMessage')

class KeepAliveServerMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 20108
    this.client = client
    this.version = 1
  }

  async encode () {}
}

module.exports = KeepAliveServerMessage