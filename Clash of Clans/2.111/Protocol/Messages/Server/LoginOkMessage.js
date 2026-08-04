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
    this.writeInt(2) // ServerMajorVersion
    this.writeInt(111) // ServerBuild
    this.writeInt(0) // ContentVersion
    this.writeString('stage') // ServerEnvironment
    this.writeInt(1) // PlayTimeSeconds
    this.writeInt(70) // SessionCount
    this.writeInt(0) // DaysSinceStartedPlaying
    this.writeString('1475268786112433') // FacebookAppId
    //if ( !ByteStream::isAtEnd((LoginOkMessage *)((char *)this + 8)) )
    //{
      this.writeString('1620660784191') // ServerTime
      this.writeString('1620660618000') // AccountCreatedDate
    //}
    //result = ByteStream::isAtEnd((LoginOkMessage *)((char *)this + 8));
    //if ( !result )
    //{
      this.writeInt(0) // StartupCooldownSeconds
      //*((_DWORD *)this + 27) = result;
    //}
  }
}

module.exports = LoginOkMessage
