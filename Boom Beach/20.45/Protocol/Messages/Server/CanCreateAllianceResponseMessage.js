const PiranhaMessage = require('../../PiranhaMessage')

class CanCreateAllianceResponseMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24356
    this.client = client
    this.version = 9
  }

  async encode () {
    this.writeInt(0) // if this is bigger than 1 then u get the thing where u have to wait a few secs
  }
}

module.exports = CanCreateAllianceResponseMessage