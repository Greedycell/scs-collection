const PiranhaMessage = require('../../PiranhaMessage')
const AvailableServerCommand = require('../Server/AvailableServerCommand')

class ChangeAvatarNameMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 10212
    this.version = 1
  }

  async decode () {
    this.data = {}

    this.data.some = this.readString()
    this.data.thing = this.readString()

    console.log(this.data)
  }

  async process () {
    await new AvailableServerCommand(this.client, 201, this.data.some).send()
  }
}

module.exports = ChangeAvatarNameMessage