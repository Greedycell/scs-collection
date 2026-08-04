const LogicBase = require('./LogicBase')
const starting_home = require('../Gamefiles/level/starting_home.json')

class LogicClientHome {
  async encode (self) {
    new LogicBase().encode(self)
    self.writeLong(0, 1) // Id
    self.writeString(JSON.stringify(starting_home)) // HomeJSON
    self.writeInt(0) // ShieldDurationSeconds
    self.writeInt(0) // DefenceRating
    self.writeInt(0) // DefenceKFactor
  }
}

module.exports = LogicClientHome