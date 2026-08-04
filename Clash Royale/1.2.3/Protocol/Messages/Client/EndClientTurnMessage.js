const PiranhaMessage = require('../../PiranhaMessage')

class EndClientTurnMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14102
    this.version = 1
  }

  async decode () {
    this.data = {}
    
    this.data.Tick = this.readVInt()
    this.data.Checksum = this.readVInt()
    this.data.Count = this.readVInt()
    this.data.CommandID = this.readVInt()

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
      this.client.log(`Command ${this.data.CommandID} isn't handled!`)
    }
  }
}

module.exports = EndClientTurnMessage