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
    this.writeInt(0) // SecondsSinceLastSave
    
    new LogicClientHome().encode(this)
    new LogicClientAvatar().encode(this)
  }
}

module.exports = OwnHomeDataMessage