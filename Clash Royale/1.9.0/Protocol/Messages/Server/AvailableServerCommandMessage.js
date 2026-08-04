const PiranhaMessage = require('../../PiranhaMessage')
const LogicChangeAvatarNameCommand = require('../../Commands/Server/LogicChangeAvatarNameCommand')

class AvailableServerCommandMessage extends PiranhaMessage {
  constructor (client, commandID) {
    super()
    this.id = 24111
    this.client = client
    this.version = 1
    this.commandID = commandID
  }

  async encode () {
    var commands = {
      201: LogicChangeAvatarNameCommand,
    }

    if (this.commandID in commands) {
      this.writeVInt(this.commandID)
      await new commands[this.commandID]().encode(this)
      //console.log(this.commandID)
    }
  }
}

module.exports = AvailableServerCommandMessage