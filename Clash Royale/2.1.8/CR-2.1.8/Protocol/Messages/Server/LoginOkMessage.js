const PiranhaMessage = require('../../PiranhaMessage')

class LoginOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 22280
    this.client = client
    this.version = 4
  }

  async encode () {
    this.writeInt(0) // HighID
    this.writeInt(1) // LowID
    this.writeInt(0) // HighID
    this.writeInt(1) // LowID
    this.writeString('nypbej3nc7cbcz3bk2mcxtx2x6ba') // Token
    this.writeString('')
    this.writeString('')
    this.writeVInt(3) // Major
    this.writeVInt(830) // Minor
    this.writeVInt(830) // Minor
    this.writeVInt(0) // Build
    this.writeString('prod') // Environment
    this.writeVInt(0)//sessionCount
    this.writeVInt(0)//playTimeSeconds
    this.writeVInt(0)//daysSinceStartedPlaying
    this.writeString('1475268786112433')//Fb id
    this.writeString(Date.now().toString())//Servertime
    this.writeString(Date.now().toString())//accountCreatedDate
    this.writeByte(0)
    this.writeString('')
    this.writeString('')
    this.writeString('')
    this.writeString('IT')//State
    this.writeString('CR City')//City
    this.writeString('20')//Country code
    this.writeVInt(1)
    this.writeVInt(455500)
    this.writeVInt(115500)
    this.writeVInt(2)
    this.writeString('')//GameAssetsURL
    this.writeString('https://game-assets.clashroyaleapp.com')//GameAssetsURL
    this.writeVInt(1)
    this.writeString('') //EventAssetsURL
  }
}

module.exports = LoginOkMessage