const PiranhaMessage = require('../../PiranhaMessage')

class LoginOkMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 29360
    this.client = client
    this.version = 4
  }

  async encode () {
    this.writeLong(0, 1) // HighID, LowID
    this.writeLong(0, 1) // HighID, LowID
    this.writeString('nypbej3nc7cbcz3bk2mcxtx2x6bazd6xnt7ec7xs') // Token
    this.writeString(null)
    this.writeString(null)
    this.writeVInt(3) // Major
    this.writeVInt(2557) // Build
    this.writeVInt(2556) // Build
    this.writeVInt(7) // Minor
    this.writeString('prod') // Environment
    this.writeVInt(1)
    this.writeVInt(70)
    this.writeVInt(0)
    this.writeString('1475268786112433')
    this.writeString('1620660784191')
    this.writeString('1620660618000')
    this.writeVInt(0)
    this.writeString(null)
    this.writeString(null)
    this.writeString(null)
    this.writeString('UA')
    this.writeString('Odessea')
    this.writeString('17')
    this.writeVInt(1)
    this.writeVInt(464775)
    this.writeVInt(307326)
    this.writeVInt(2)
    this.writeString('https://game-assets.clashroyaleapp.com')
    this.writeString('https://99faf1e355c749a9a049-2a63f4436c967aa7d355061bd0d924a1.ssl.cf1.rackcdn.com')
    this.writeVInt(2)
    this.writeString('https://event-assets.clashroyale.com')
    this.writeString('https://56f230c6d142ad8a925f-b174a1d8fb2cf6907e1c742c46071d76.ssl.cf2.rackcdn.com/event-assets')
    this.writeBytes(Buffer.from([221,150,130,32,20,70,159,168,22,216,207,212,101,33,21,164,56,148,89,112,55,90,41,36,211,106,210,33,120,250,209,230,114,239,181,246,249,206,197,209,159,175,35,87,137,162,90,4,98,204,180,212,9,2,150,121,62,138,82,238,132,47,27,145,214,154,33,0,133,231,1,51,2,176,52,51,194,99,21,33,10,46,167,69,223,110,51,200,123,174,242,117,209,243,234,224,9,100,138,206,135,23,71,237,255,0,177,81,122,176,113,138,93,18,30,70,68,89,85,108,222,94,239,32,221,115,184,220,101,53,30,247,94,6,171,70,238,201,148,124,51,93,152,249,51,71,228,73,76,214,158,85,239,150,46,15,222,99,250,221,27,89,229,27,86,119,236,69,80,223,250,238,188,161,80,238,187,59,167,202,18,125,127,49,77,108,18,198,158,165,37,184,242,161,153,197,232,17,204,241,86,185,238,95,212,220,42,213,108,129,193,128,223,150,247,223,235,218,193,6,129,105,161,38,237,42,247,178,125,224,89,136,219,241,186,92,132,37,119,131,56,58,127,28,165,157,141,158,9,158,188,142,108,80,145,52,244,159,246,15,33,165,111,67,0,0,0,0,0,0,0,0]))
    this.writeBoolean(false)
    this.writeBoolean(false)
    this.writeBoolean(false)
    this.writeVInt(0)
    this.writeStringReference('https://play.google.com/store/apps/details?id=com.supercell.clashroyal')
    this.writeString('AAABeVbpCuIKAzqTAC6XKQ==')
    this.writeVInt(0)
  }
}

module.exports = LoginOkMessage
