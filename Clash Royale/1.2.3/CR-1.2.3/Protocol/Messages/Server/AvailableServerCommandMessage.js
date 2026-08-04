const PiranhaMessage = require('../../PiranhaMessage')
const LogicChangeAvatarNameCommand = require('../../Commands/Server/LogicChangeAvatarNameCommand')
const LogicLeaveAllianceCommand = require('../../Commands/Server/LogicLeaveAllianceCommand')

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
      205: LogicLeaveAllianceCommand
    }

    if (this.commandID in commands) {
      this.writeVInt(this.commandID)
      const command = new commands[this.commandID]()
      await command.encode(this)
      this.client.log(`Gotcha ${this.commandID} (${command.constructor.name}) command! `)
    }
  }
}

module.exports = AvailableServerCommandMessage