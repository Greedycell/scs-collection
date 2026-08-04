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
    this.writeString('') // Facebook
    this.writeString('') // Gamecenter
    this.writeVInt(3) // Major
    this.writeVInt(377) // Minor
    this.writeVInt(0) // Build
    this.writeString('dev') // Environment
    this.writeVInt(0) // SessionCount
    this.writeVInt(0) //PlayTimeSeconds
    this.writeVInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppID
    this.writeString(Date.now().toString()) // Server Time
    this.writeString(Date.now().toString()) // AccountCreatedDate
    this.writeVInt(0)
    this.writeString("G:1")
    this.writeString("")
    this.writeString("DE")
    this.writeString("Berlin")
    this.writeString("")
    this.writeString("")
    this.writeString("")
  }
}

module.exports = LoginOkMessage