const PiranhaMessage = require('../../PiranhaMessage')

class GlobalPlayersMessage extends PiranhaMessage {
  constructor (client) {
    super()
    this.id = 24403
    this.client = client
    this.version = 1
  }

  async encode () {
    this.writeVInt(1) // Count

    // RankingEntry
    {
      this.writeVInt(1) // HighID
      this.writeVInt(1) // LowID
      this.writeString('Berkan') // Name
      this.writeVInt(1) // i + 1
      this.writeVInt(5000) // Trophies
      this.writeVInt(200)
      this.writeVInt(0)
      this.writeVInt(0)
      this.writeVInt(0)

      this.writeVInt(12) // Level

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeString("DE")
      this.writeLong(0, 1) // HighID, LowID

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeByte(0)
      this.writeByte(0)
      this.writeByte(0)

      this.writeVInt(1) // InClan (0 = No, 1 = Yes)
      this.writeLong(0, 1)
      this.writeString('GobelinLand') // AllianceName
      this.writeByte(16)
      this.writeByte(26) // AllianceBadge
    }

    this.writeInt(0)
    this.writeInt(2505600) // Time
  }
}

module.exports = GlobalPlayersMessage