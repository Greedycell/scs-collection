const PiranhaMessage = require('../../PiranhaMessage')

class BattleEventMessage extends PiranhaMessage {
  constructor (client, data) {
    super()
    this.id = 22952
    this.client = client
    this.version = 1
    this.data = data
  }

  async encode () {
    this.writeVInt(this.data.Type)
    this.writeVInt(this.data.HighId)
    this.writeVInt(this.data.LowId)
    this.writeVInt(1)
    this.writeVInt(this.data.Tick)
    this.writeVInt(this.data.Unknown3)
    this.writeVInt(this.data.Value1)
    this.writeVInt(this.data.Value2)

    if (this.data.Type == 6)
    {
        this.writeVInt(this.data.Unknown)
        this.writeVInt(this.data.HandIndex)
        this.writeVInt(this.data.Unknown2)
    }
  }
}

module.exports = BattleEventMessage