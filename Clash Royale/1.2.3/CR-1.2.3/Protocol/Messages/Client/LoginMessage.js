const PiranhaMessage = require('../../PiranhaMessage')
const LoginOkMessage = require('../Server/LoginOkMessage')
const OwnHomeDataMessage = require('../Server/OwnHomeDataMessage')
const LoginFailedMessage = require('../Server/LoginFailedMessage')

const config = require('../../../config.json')

class LoginMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 10101
    this.version = 1
  }

  async decode () {
    this.data = {}

    this.data.HighID = this.readInt()
    this.data.LowID = this.readInt()
    this.data.Token = this.readString()
    this.data.Major = this.readVInt()
    this.data.Build = this.readVInt()
    this.data.Content = this.readVInt()

    //console.log(this.data)
  }

  async process () {
    if (this.data.Major !== parseInt(config.Server.Version.split('.')[0]) || this.data.Content !== parseInt(config.Server.Version.split('.')[1])) {
      await new LoginFailedMessage(this.client, 3, `Your version doesn't match the server version.\n Client Version: ${this.data.Major}.${this.data.Content}\n Server Version: ${config.Server.Version}`).send()
      return
    }

    await new LoginOkMessage(this.client).send()
    await new OwnHomeDataMessage(this.client).send()
  }
}

module.exports = LoginMessage