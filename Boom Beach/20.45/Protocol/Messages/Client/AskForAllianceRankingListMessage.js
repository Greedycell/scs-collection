const PiranhaMessage = require('../../PiranhaMessage')
const AllianceRankingListMessage = require('../Server/AllianceRankingListMessage')

class AskForAllianceRankingListMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14401
    this.version = 13
  }

  async decode () {
    this.data = {}

    this.data.Unknown1 = this.readBoolean()
    if (this.data.Unknown1) {
      this.data.AllianceId = this.readLong()
    }

    //console.log(this.data)
  }

  async process () {
    await new AllianceRankingListMessage(this.client).send()
  }
}

module.exports = AskForAllianceRankingListMessage