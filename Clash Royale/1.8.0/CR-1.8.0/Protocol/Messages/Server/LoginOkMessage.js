const PiranhaMessage = require('../../PiranhaMessage')

class LoginOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 20104
    this.client = client
    this.version = 4
  }

  async encode () {
    this.writeLong(0, 1) // HighID, LowID
    this.writeLong(0, 1) // HighID, LowID
    this.writeString('nypbej3nc7cbcz3bk2mcxtx2x6ba') // Token
    this.writeString('') // Facebook
    this.writeString('') // Gamecenter
    this.writeVInt(3) // Major
    this.writeVInt(193) // Minor
    this.writeVInt(0) // Build
    this.writeString('prod') // Environment
    this.writeVInt(0) // SessionCount
    this.writeVInt(0) //PlayTimeSeconds
    this.writeVInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppID
    this.writeString(Date.now().toString()) // Server Time
    this.writeString(Date.now().toString()) // AccountCreatedDate
    this.writeVInt(0)
    this.writeString('') // GoogleServiceID
    this.writeString('')
    this.writeString('')
    this.writeString('') // Region
    this.writeString('http://7166046b142482e67b30-2a63f4436c967aa7d355061bd0d924a1.r65.cf1.rackcdn.com') // ContentURL
    this.writeString('https://event-assets.clashroyaleapp.com') // EventAssetsURL
    this.writeByte(1)
  }
}

module.exports = LoginOkMessage