const LogicTimer = require('./LogicTimer')

class LogicClientHome {
  async encode (self) {
    self.writeLong(0, 1) // Id
    self.writeVInt(0)
    self.writeVInt(0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeVInt(0)
    self.writeVInt(1) // DeckCount
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeVInt(0) // SpellCount
    self.writeVInt(0) // CurrentSort
    self.writeVInt(0) // SelectedDeck
    self.writeVInt(4) // ChestCount
    {
      self.writeBoolean(false) // true = chests, false = no chests
      
      self.writeBoolean(true) // true = chests, false = no chests
      self.writeDataReference(19, 36) // 1 = ChestId 
      self.writeBoolean(false) // Unlocked
      self.writeBoolean(false) // No Menu
      self.writeBoolean(false) // Jump Animation
      if (false) {
        self.writeBoolean(true)
        new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
      } else {
        self.writeBoolean(false)
      }
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)

      self.writeBoolean(true) // true = chests, false = no chests
      self.writeDataReference(19, 36) // 1 = ChestId 
      self.writeBoolean(false) // Unlocked
      self.writeBoolean(false) // No Menu
      self.writeBoolean(false) // Jump Animation
      if (false) {
        self.writeBoolean(true)
        new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
      } else {
        self.writeBoolean(false)
      }
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)

      self.writeBoolean(true) // true = chests, false = no chests
      self.writeDataReference(19, 36) // 1 = ChestId 
      self.writeBoolean(false) // Unlocked
      self.writeBoolean(false) // No Menu
      self.writeBoolean(false) // Jump Animation
      if (false) {
        self.writeBoolean(true)
        new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
      } else {
        self.writeBoolean(false)
      }
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
    }
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeVInt(0)
    self.writeBoolean(false)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeBoolean(false)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    for (let i = 0; i < 8; i++) self.writeVInt(0)
    self.writeVInt(2) // PageOpened
    self.writeVInt(1) // OldLevel
    self.writeDataReference(54, 1) // OldArena
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeVInt(0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeStringReference(null)
    self.writeVInt(0) // SpellListCount
  }
}

module.exports = LogicClientHome
