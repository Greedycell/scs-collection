const PiranhaMessage = require('../../PiranhaMessage')

class LoginOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 20104
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeLong(0, 1) // AccountId
    this.writeLong(0, 1) // AccountId
    this.writeString('nypbej3nc7cbcz3bk2mcxtx2x6bazd6xnt7ec7xs') // PassToken
    this.writeString(null) // GamecenterId
    this.writeString(null) // FacebookId
    this.writeVInt(2) // ServerMajorVersion
    this.writeVInt(1835) // ServerBuild
    this.writeVInt(1835) // ServerBuild
    this.writeVInt(1) // ContentVersion
    this.writeString('prod') // ServerEnvironment
    this.writeVInt(1) // PlayTimeSeconds
    this.writeVInt(70) // SessionCount
    this.writeVInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppId
    this.writeString('1620660784191') // ServerTime
    this.writeString('1620660618000') // AccountCreatedDate
    this.writeVInt(0) // StartupCooldownSeconds
  }
}

module.exports = LoginOkMessage
