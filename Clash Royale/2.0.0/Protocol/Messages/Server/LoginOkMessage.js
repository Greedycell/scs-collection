const PiranhaMessage = require('../../PiranhaMessage')

class LoginOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 20104
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeLong(0, 1) // HighID, LowID
    this.writeLong(0, 1) // HighID, LowID
    this.writeString('nypbej3nc7cbcz3bk2mcxtx2x6ba') // Token
    this.writeString(null)
    this.writeString(null)
    this.writeVInt(3) // Major
    this.writeVInt(690) // Minor
    this.writeVInt(690) // Minor
    this.writeVInt(0) // Build
    this.writeString('prod') // Environment
    this.writeVInt(1) //SessionCount
    this.writeVInt(0) // PlayTimeSeconds
    this.writeVInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppID
    this.writeString(Date.now().toString())// ServerTime
    this.writeString(Date.now().toString()) // AccountCreatedDate
    this.writeByte(0)
    this.writeString(null)
    this.writeString(null)
    this.writeString(null)
    this.writeString(null) // State
    this.writeString(null) // City
    this.writeString('http://7166046b142482e67b30-2a63f4436c967aa7d355061bd0d924a1.r65.cf1.rackcdn.com') // ContentURL
    this.writeString('https://event-assets.clashroyale.com') // EventAssetsURL
    this.writeByte(1)
  }
}

module.exports = LoginOkMessage