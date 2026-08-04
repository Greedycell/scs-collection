const PiranhaMessage = require('../../PiranhaMessage')

class SectorHeartbeatMessage extends PiranhaMessage {
  constructor (client, turn, commands) {
    super()
    this.id = 21902
    this.client = client
    this.version = 1
    this.turn = turn
    this.commands = commands
  }

  async encode () {
    this.writeVInt(this.turn)
    this.writeVInt(0)

    this.writeVInt(this.commands)

    for (var i = 0; i < this.commands; i++) this.writeByte/*s*/(0/*Commands.Dequeue()*/)
  }
}

module.exports = SectorHeartbeatMessage