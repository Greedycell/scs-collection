const PiranhaMessage = require('../../PiranhaMessage')
const AllianceDataMessage = require('../Server/AllianceDataMessage')

class ChangeAllianceSettingMessage extends PiranhaMessage {
  constructor (bytes, client) {
    super(bytes)
    this.client = client
    this.id = 14316
    this.version = 1
  }

  async decode () {
    this.data = {}

    this.data.AllianceDescription = this.readString()
    this.data.Unknown = this.readVInt() // Byte?
    this.data.AllianceBadge = this.readVInt()
    this.data.AllianceType = this.readVInt()
    this.data.AllianceTrophies = this.readVInt()
    this.data.AllianceOrigin = this.readVInt()

    //console.log(this.data)
  }

  async process () {}
}

module.exports = ChangeAllianceSettingMessage