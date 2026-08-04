const LogicMapRegion = require('./LogicMapRegion')

class LogicPlayerMap {
  async encode (self) {
    //new LogicMapRegion().encode(self)

    self.writeInt(0)
    self.writeBoolean(false)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeBoolean(false)
  }
}

module.exports = LogicPlayerMap