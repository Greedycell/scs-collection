const PiranhaMessage = require('../../PiranhaMessage')

class AllianceStreamMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24311
    this.client = client
    this.version = 11
  }

  async encode () {
    /*
    4 = AllianceEventStreamEntry
    3 = JoinRequestAllianceStreamEntry
    2 = ChatStreamEntry
    */

    this.writeInt(1) // EntryCount

    let streamEntryType = 2
    this.writeInt(streamEntryType) // StreamEntryType

    // StreamEntryFactory::createStreamEntryByType
    {
      // AllianceEventStreamEntry
      {
        this.writeInt(0)  // MessageId // when a db is added, this should be a new msg id instead of the same id to prevent same msg going into the box
        this.writeLong(0, 0) // Id
        this.writeLong(0, 0) // Id
        this.writeString('GoldenBeach')
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
            this.writeString('test message that came when u logged in')
            break
        }
      }
    }
  }
}

module.exports = AllianceStreamMessage