const PiranhaMessage = require('../../PiranhaMessage')

class CancelChallengeOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24124
    this.client = client
    this.version = 1
  }

  async encode () {}
}

module.exports = CancelChallengeOkMessage