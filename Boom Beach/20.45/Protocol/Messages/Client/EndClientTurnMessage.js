const PiranhaMessage = require('../../PiranhaMessage')
const OutOfSyncMessage = require('../Server/OutOfSyncMessage')

class EndClientTurnMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14102
    this.version = 1
  }

  async decode () {
    this.data = {}
    
    this.readInt()
    this.readInt()
    this.readInt()
    this.data.Count = this.readInt()
    this.data.CommandID = this.readInt()

    //console.log(this.data)
  }

  async process () {
    var Commands = {
    }

    if (this.data.CommandID in Commands) {
      var command = new Commands[this.data.CommandID]
      this.client.log(`Command ${this.data.CommandID} (${command.constructor.name}) handled!`)
      command.decode(this)
      command.process(this)
    }
    else {
      if (String(this.data.CommandID).startsWith('-')) {
        await new OutOfSyncMessage(this.client).send()
        return
      }

      this.client.log(`Command ${this.data.CommandID} isn't handled!`)
    }
  }
}

module.exports = EndClientTurnMessage