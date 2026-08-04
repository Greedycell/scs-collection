const PiranhaMessage = require('../../PiranhaMessage')
const LogicClientHome = require('../../../Logic/LogicClientHome')
const LogicClientAvatar = require('../../../Logic/LogicClientAvatar')

class OwnHomeDataMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24101
    this.client = client
    this.version = 10
  }

  async encode () {
    this.writeInt(0)
    new LogicClientHome().encode(this)
    new LogicClientAvatar().encode(this)
    this.writeInt(0)
    this.writeInt(0)
    this.writeInt(0)
    this.writeInt(0)
    this.writeInt(0)
    this.writeString(null)
    this.writeString(null)
  }
}

module.exports = OwnHomeDataMessage