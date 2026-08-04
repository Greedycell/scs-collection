const PiranhaMessage = require('../../PiranhaMessage')

class LoginFailedMessage extends PiranhaMessage {
  constructor (client, errorCode, reason) {
    super()
    this.id = 20103
    this.client = client
    this.version = 4
    this.errorCode = errorCode
    this.reason = reason
  }

  async encode () {
    /*
    Error Codes:
    3 = Custom Message
    8 = Update Available
    9 = Connection Error
    10 = Maintenance
    11 = Banned
    */

    this.writeVInt(this.errorCode) // ErrorCode
    this.writeString('') // Fingerprint
    this.writeString(null) // RedirectURL
    this.writeString('') // ContentURL
    this.writeString('') // UpdateURL
    this.writeString(this.reason) // Reason
    this.writeInt(0) // MaintenanceSeconds
  }
}

module.exports = LoginFailedMessage