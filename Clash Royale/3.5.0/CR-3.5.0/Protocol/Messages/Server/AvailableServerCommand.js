const PiranhaMessage = require('../../PiranhaMessage')
const LogicChangeAvatarNameCommand = require('../../Commands/Server/LogicChangeAvatarNameCommand')

class AvailableServerCommand extends PiranhaMessage {
  constructor (client, commandID, arg1) {
    super()
    this.id = 24111
    this.client = client
    this.version = 1
    this.commandID = commandID
    this.arg1 = arg1
  }

  async encode () {
    var commands = {
      201: LogicChangeAvatarNameCommand,
    }

    if(this.commandID in commands){
      this.writeVInt(this.commandID)
      await new commands[this.commandID]().encode(this, this.arg1)
      console.log(this.commandID)
    }
  }
}

module.exports = AvailableServerCommand