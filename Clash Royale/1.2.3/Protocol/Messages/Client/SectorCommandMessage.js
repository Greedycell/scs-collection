const PiranhaMessage = require('../../PiranhaMessage')
const SectorHeartbeatMessage = require('../Server/SectorHeartbeatMessage')
const LogicBattle = require('../../../Battle/LogicBattle')

class SectorCommandMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 12904
    this.version = 1
  }

  async decode () {
    this.data = {}
    
    this.readVInt()
    this.data.Tick = this.readVInt()
    this.data.Count = this.readVInt()

    //console.log(this.data)
  }

  async process () {
    this.client.LastSectorCommand = Date.now() / 1000 | 0

    //if (this.data.Count < 0 || this.data.Count > 128) return

    const battle = new LogicBattle()
    await new SectorHeartbeatMessage(this.client, battle.Tick(300), 0).send()

    for (var i = 0; i < this.data.Count; i++)
    {
        var type = this.readVInt()

        if (type >= 500) break;

        //if (LogicCommandManager.Commands.ContainsKey(type))
          /*if (Activator.CreateInstance(LogicCommandManager.Commands[type], Device,
                  Reader) is
              LogicCommand
              command)
          {
              command.Decode()
              command.Encode()
              command.Process()*/

              this.client.log(`SectorCommand ${type} with Tick ${this.data.Tick} has been processed.`);
        //else {
        //    this.client.Log(`"SectorCommand ${type} is unhandled.`);
    }
  }
}

module.exports = SectorCommandMessage