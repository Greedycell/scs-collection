const cardUtils = require('../Utils/cardUtils')
const utils = require('../Utils')
const LogicTimer = require('./LogicTimer')

class LogicClientHome {
  async encode (self) {
    this.decks = [
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007] // Deck 1
    ]
    this.selectedDeck = 0
    this.cards = [
        { ID: 1, level: 0, xpPoints: 1 },
        { ID: 2, level: 0, xpPoints: 1 },
        { ID: 3, level: 0, xpPoints: 1 },
        { ID: 4, level: 0, xpPoints: 1 },
        { ID: 5, level: 0, xpPoints: 1 },
        { ID: 6, level: 0, xpPoints: 1 },
        { ID: 7, level: 0, xpPoints: 1 },
        { ID: 8, level: 0, xpPoints: 1 }
    ]

    self.writeLong(0, 1) // Id
    self.writeVInt(0)
    self.writeVInt(0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeVInt(0)
    
    self.writeVInt(0)
    /*self.writeVInt(this.decks.length)
    this.decks.forEach(deck => {
        self.writeVInt(deck.length)
        deck.forEach(card => {
            self.writeVInt(card)
        })
    })*/

    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    /*self.writeByte(0xFF)
    let currentDeck = this.decks[this.selectedDeck]
    currentDeck.forEach(cardSCID => {
        let card = utils.findObjectByKey(this.cards, 'ID', cardUtils.SCIDtoInstanceID(cardSCID))
        self.writeDataReference(card.ID, card.level)
        self.writeVInt(0)
        self.writeVInt(card.xpPoints)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeBoolean(false)
        self.writeBoolean(false)
    })*/

    self.writeVInt(0)
    /*self.writeVInt(this.cards.length - 8)
    this.cards.forEach(card => {
        if (!currentDeck.includes(cardUtils.instanceIDtoSCID(card.ID))) {
            self.writeDataReference(card.ID, card.level)
            self.writeVInt(0)
            self.writeVInt(card.xpPoints)
            self.writeVInt(0)
            self.writeVInt(0)
            self.writeVInt(0)
            self.writeBoolean(false)
            self.writeBoolean(false)
        }
    })*/

    self.writeVInt(this.selectedDeck)

    self.writeVInt(4) // ChestCount
    {
        self.writeBoolean(false)
    }
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeDataReference(0, 0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(false)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
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
    self.writeBoolean(false)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    new LogicTimer().encode(self, 239940, 346065, Date.now() / 1000 | 0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0) // ByteStreamHelper::encodeSpellList
    self.writeBoolean(false)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)

    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeBoolean(false)
    /*self.writeByte(0xFF)
    currentDeck.forEach(cardSCID => {
        let card = utils.findObjectByKey(this.cards, 'ID', cardUtils.SCIDtoInstanceID(cardSCID))
        self.writeDataReference(card.ID, card.level)
        self.writeVInt(0)
        self.writeVInt(card.xpPoints)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeBoolean(false)
        self.writeBoolean(false)
    })*/


    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(true)
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeVInt(0)
    self.writeBoolean(false)
    self.writeBoolean(false)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
  }
}

module.exports = LogicClientHome