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
    this.writeInt(6) // ServerMajorVersion
    this.writeInt(186) // ServerBuild
    this.writeInt(1) // ContentVersion
    this.writeString('prod') // ServerEnvironment
    this.writeInt(1) // PlayTimeSeconds
    this.writeInt(70) // SessionCount
    this.writeInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppId
    this.writeString('1620660784191') // ServerTime
    this.writeString('1620660618000') // AccountCreatedDate
    this.writeInt(0) // StartupCooldownSeconds
    this.writeString('108457211027966753069')
  }
}

module.exports = LoginOkMessage
