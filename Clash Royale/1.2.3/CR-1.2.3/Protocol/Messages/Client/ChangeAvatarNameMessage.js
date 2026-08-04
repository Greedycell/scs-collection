const PiranhaMessage = require('../../PiranhaMessage')
const AvailableServerCommandMessage = require('../Server/AvailableServerCommandMessage')

class ChangeAvatarNameMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 10212
    this.version = 1
  }

  async decode () {
    this.data = {}

    this.data.Name = this.readString()

    //console.log(this.data)
  }

  async process () {
    this.client.name = this.data.Name
    
    await new AvailableServerCommandMessage(this.client, 201).send()
  }
}

module.exports = ChangeAvatarNameMessage