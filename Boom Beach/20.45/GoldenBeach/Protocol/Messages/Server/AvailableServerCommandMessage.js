const PiranhaMessage = require('../../PiranhaMessage')
const LogicJoinAllianceCommand = require('../../Commands/Server/LogicJoinAllianceCommand')

class AvailableServerCommandMessage extends PiranhaMessage {
  constructor (client, commandID, data) {
    super()
    this.id = 24111
    this.client = client
    this.version = 10
    this.commandID = commandID
    this.data = data
  }

  async encode () {
    var commands = {
      1: LogicJoinAllianceCommand
    }

    if (this.commandID in commands) {
      this.writeVInt(this.commandID)
      const command = new commands[this.commandID]()
      await command.encode(this)
      this.client.log(`Gotcha ${this.commandID} (${command.constructor.name}) command!`)
    }
  }
}

module.exports = AvailableServerCommandMessage