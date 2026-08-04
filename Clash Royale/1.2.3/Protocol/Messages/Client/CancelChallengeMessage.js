const PiranhaMessage = require('../../PiranhaMessage')
const CancelChallengeOkMessage = require('../Server/CancelChallengeOkMessage')

class CancelChallengeMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14123
    this.version = 1
  }

  async decode () {}

  async process () {
    await new Promise(resolve => setTimeout(resolve, 500))
    await new CancelChallengeOkMessage(this.client).send()
  }
}

module.exports = CancelChallengeMessage