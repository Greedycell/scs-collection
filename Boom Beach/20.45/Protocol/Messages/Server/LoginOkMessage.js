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
    this.writeInt(20) // ServerMajorVersion
    this.writeInt(45) // ServerBuild
    this.writeInt(1) // ContentVersion
    this.writeString('stage') // ServerEnvironment
    this.writeString('EN') // CountryCode
    this.writeString(null)
    this.writeString(null)
    this.writeString(null)
    this.writeString(null)
    this.writeString(null)
  }
}

module.exports = LoginOkMessage