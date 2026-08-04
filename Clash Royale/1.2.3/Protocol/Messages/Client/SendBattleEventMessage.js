const PiranhaMessage = require('../../PiranhaMessage')
const BattleEventMessage = require('../Server/BattleEventMessage')

class SendBattleEventMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 12951
    this.version = 1
  }

  async decode () {
    this.data = {}
    
    this.data.Type = this.readVInt()
    this.data.HighId = this.readVInt()
    this.data.LowId = this.readVInt()
    this.readVInt()
    this.data.Tick = this.readVInt()
    this.data.Unknown3 = this.readVInt()
    this.data.Value1 = this.readVInt()
    this.data.Value2 = this.readVInt()

    //console.log(this.data)
  }

  async process () {
    switch (this.data.Type)
    {
        case 3:
        {
          await new BattleEventMessage(this.client, this.data).send()
          break
        }
    }
  }
}

module.exports = SendBattleEventMessage