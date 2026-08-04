const PiranhaMessage = require('../../PiranhaMessage')

class WaitingToGoHomeMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24112
    this.client = client
    this.version = 10
  }

  async encode () {
    this.writeInt(0)
  }
}

module.exports = WaitingToGoHomeMessage