const cardUtils = require('../Utils/cardUtils')
const utils = require('../Utils')

class ClientHome {
  async encode (self) {
    this.decks = [
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 1
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

    self.writeLong(0, 1) // HighID, LowID
    self.writeVInt(127)
    self.writeVInt(257)
    self.writeVInt(3250)
    self.writeVInt(168720)
    self.writeVInt(Date.now() / 1000 | 0)
    self.writeByte(1)
    
    self.writeByte(this.decks.length)
    this.decks.forEach(deck => {
        self.writeByte(deck.length)
        deck.forEach(card => {
            self.writeVInt(card)
        })
    })
    self.writeByte(0xFF)
    let currentDeck = this.decks[this.selectedDeck]
    currentDeck.forEach(cardSCID => {
        let card = utils.findObjectByKey(this.cards, 'ID', cardUtils.SCIDtoInstanceID(cardSCID))

        self.writeVInt(card.ID)
        self.writeVInt(card.level)
        self.writeVInt(0)
        self.writeVInt(card.xpPoints)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
    })
    self.writeVInt(this.cards.length - 8)
    this.cards.forEach(card => {
        if (!currentDeck.includes(cardUtils.instanceIDtoSCID(card.ID))) {
            self.writeVInt(card.ID)
            self.writeVInt(card.level)
            self.writeVInt(0)
            self.writeVInt(card.xpPoints)
            self.writeVInt(0)
            self.writeVInt(0)
            self.writeVInt(0)
        }
    })
    self.writeVInt(this.selectedDeck)
    
    self.writeHex('ff0d007f000000008301007f0000000017007f000000008f01007f0000000001007f0000000007007f000000009301007f000000000c007f000000001d9e137f')
    self.writeVInt(1511717732)
    self.writeByte(1)
    self.writeByte(0)

    self.writeVInt(0) // EventCount

    self.writeInt(0)
    self.writeByte(2)
    self.writeByte(0)
    self.writeVInt(1511424000) // TIMESTAMP
    self.writeInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeByte(2)
    self.writeString('{}') // CardRelease
    self.writeByte(4)
    self.writeString('{}') // ClanChest

    self.writeByte(0)

    // ChestSlots
    self.writeByte(4)
    {
        self.writeByte(0)
    }

    self.writeByte(0)
    self.writeByte(0)
    self.writeByte(-64)

    self.writeByte(0)
    self.writeByte(0)
    self.writeByte(-64)

    self.writeHex('000000000000000000000000')
    self.writeVInt(0) // CrownChestCount
    self.writeHex('0000007fb0d40380d3c701')
    self.writeVInt(Date.now() / 1000 | 0)
    self.writeInt(127)
    self.writeByte(3)
    self.writeInt(0)
    self.writeInt(2)

    self.writeVInt(1) // OldLevel
    self.writeByte(54)
    self.writeVInt(1) // OldArena
    self.writeByte(6)
    self.writeVInt(335)
    self.writeByte(1)

    self.writeVInt(72000)
    self.writeVInt(72000)
    self.writeVInt(1512172799)

    self.writeHex('0000007f00007f00007f13109605000000000000011a15010a00000000f807')

    this.decks[this.selectedDeck].forEach(cardSCID => {
        let card = utils.findObjectByKey(this.cards, 'ID', cardUtils.SCIDtoInstanceID(cardSCID))

        self.writeVInt(card.ID)
        self.writeVInt(card.level)
        self.writeVInt(0)
        self.writeVInt(card.xpPoints)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
    })

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeHex('0000058cd2f83e8dd2f83e8ed2f83e89d2f83e8ad2f83e0800019081a1fe0b000201018ae6bf3302018a8919005a17ca0c5a1bbe8cb2be9d0a000000000000000000010000')
    self.writeVInt(10)
    self.writeVInt(25)
    self.writeVInt(50)
    self.writeHex('01008f050003010004')

    self.writeVInt(189)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(0)
    self.writeInt(13)
    self.writeHex('0202040104020200040306010400020004000204050304000502000008')

    self.writeByte(0) // ShopCont
    self.writeByte(0)
    self.writeByte(0) // ShopEventComponent

    self.writeInt(127)
    self.writeVInt(1109)
    self.writeByte(0)
  }
}

module.exports = ClientHome