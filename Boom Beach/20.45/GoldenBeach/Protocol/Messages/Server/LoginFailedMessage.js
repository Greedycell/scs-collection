const PiranhaMessage = require('../../PiranhaMessage')

class LoginFailedMessage extends PiranhaMessage {
  constructor (client, errorCode, reason) {
    super()
    this.id = 20103
    this.client = client
    this.version = 1
    this.errorCode = errorCode
    this.reason = reason
  }

  async encode () {
    /*
    Any invalid code will do a custom message error.
    7  = Content Patch
    8  = Update Available
    10 = Maintenance
    11 = Banned
    12 = Debug mode mismatch
    13 = Account locked
    */
    this.writeInt(this.errorCode) // ErrorCode
    this.writeString('e5caef46c95a3b14f211956d7ec21c3cea6495c9') // ResourceFingerprintData
    this.writeString(null)
    this.writeString(null) // ContentURL
    this.writeString(null) // UpdateURL
    this.writeString(this.reason) // Reason
    this.writeInt(0) // RemainingTime
  }
}

module.exports = LoginFailedMessage