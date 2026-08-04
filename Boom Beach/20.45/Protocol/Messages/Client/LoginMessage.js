const PiranhaMessage = require('../../PiranhaMessage')
const LoginOkMessage = require('../Server/LoginOkMessage')
const OwnHomeDataMessage = require('../Server/OwnHomeDataMessage')
const LoginFailedMessage = require('../Server/LoginFailedMessage')
const AllianceStreamMessage = require('../Server/AllianceStreamMessage')

class LoginMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 10101
    this.version = 1
  }

  async decode () {}

  async process () {
    await new LoginOkMessage(this.client).send()
    await new OwnHomeDataMessage(this.client).send()
    
    await new AllianceStreamMessage(this.client).send()
  }
}

module.exports = LoginMessage