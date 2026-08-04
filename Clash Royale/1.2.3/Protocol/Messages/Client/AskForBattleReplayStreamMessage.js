const PiranhaMessage = require('../../PiranhaMessage')
const HomeBattleReplayMessage = require('../Server/HomeBattleReplayMessage')

class AskForBattleReplayStreamMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14406
    this.version = 1
  }

  async decode () {}

  async process () {
    await new HomeBattleReplayMessage(this.client).send()
  }
}

module.exports = AskForBattleReplayStreamMessage