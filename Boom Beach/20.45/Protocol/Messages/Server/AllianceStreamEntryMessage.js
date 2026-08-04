const PiranhaMessage = require('../../PiranhaMessage')

class AllianceStreamEntryMessage extends PiranhaMessage {
  constructor (client, data) {
    super()
    this.id = 24312
    this.client = client
    this.version = 11
    this.data = data
  }

  async encode () {
    /*
    4 = AllianceEventStreamEntry
    3 = JoinRequestAllianceStreamEntry
    2 = ChatStreamEntry
    */

    let streamEntryType = 2
    this.writeInt(streamEntryType) // StreamEntryType

    // StreamEntryFactory::createStreamEntryByType
    {
      // AllianceEventStreamEntry
      {
        this.writeInt(Math.floor(Math.random() * 0x7FFFFFFF)) // MessageId // when a db is added, this should be a new msg id instead of the same id to prevent same msg going into the box
        this.writeLong(0, 1) // Id
        this.writeLong(0, 1) // Id
        this.writeString('Astral')
        this.writeInt(0)
        this.writeInt(0)
        this.writeInt(0)
        this.writeBoolean(false) // IsRemoved
        
        switch (streamEntryType) {
          case 4: // AllianceEventStreamEntry
            this.writeLong(0, 1)
            this.writeString('Astral')
            break
          case 3: // JoinRequestAllianceStreamEntry
            this.writeString('testuser')
            this.writeString('testuser')
            this.writeInt(1) // 0 = Rejected, 1 = Requesting, 2 = Accepted
            break
          case 2: // ChatStreamEntry
            this.writeString(this.data.Message)
            break
        }
      }
    }
  }
}

module.exports = AllianceStreamEntryMessage