const PiranhaMessage = require('../../PiranhaMessage')
const AvailableServerCommandMessage = require('../Server/AvailableServerCommandMessage')

class CreateAllianceMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14301
    this.version = 10
  }

  async decode () {
    this.data = {}

    this.readString()
    this.readString()
    
    //this.readDataReference()
    this.readInt()
    this.readInt()
    
    this.readInt()
    this.readInt()

    //console.log(this.data)
  }

  async process () {
    await new AvailableServerCommandMessage(this.client, 1, this.data).send()
  }
}

module.exports = CreateAllianceMessage