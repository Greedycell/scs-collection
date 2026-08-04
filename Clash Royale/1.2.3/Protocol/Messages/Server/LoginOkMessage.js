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
    this.writeString(null) // FacebookID
    this.writeString(null) // GamecenterID
    this.writeVInt(2) // Major
    this.writeVInt(0) // Minor
    this.writeVInt(0) // Minor
    this.writeVInt(1507) // Build
    this.writeString('dev') // Environment
    this.writeVInt(1) // SessionCount
    this.writeVInt(0) // PlayTimeSeconds
    this.writeVInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppID
    this.writeString(Date.now().toString()) // ServerTime
    this.writeString(Date.now().toString()) // AccountCreatedDate
    this.writeVInt(0)
    this.writeString(null)
  }
}

module.exports = LoginOkMessage