const cardUtils = require('../Utils/cardUtils')
const utils = require('../Utils')

class ClientHome {
  async encode (self) {
    this.decks = [
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 1
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 2
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 3
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 4
        [26000000, 26000001, 26000002, 26000003, 26000004, 26000005, 26000006, 26000007], // Deck 5
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
        { ID: 8, level: 0, xpPoints: 1 },
        { ID: 9, level: 0, xpPoints: 1 }
    ]

    self.writeLong(0, 1) // HighID, LowID

    self.writeVInt(0)

    self.writeVInt(1) // free chest id
    // timer
    for (let i = 0; i < 3; i++) self.writeVInt(0)

    self.writeVInt(0) // last login timestamp

    self.writeVInt(this.decks.length)
    this.decks.forEach(deck => {
        self.writeVInt(deck.length)
        deck.forEach(card => {
            self.writeVInt(card)
        })
    })

    for (let i = 0; i < 8; i++) {
      self.writeBoolean(true)
    }

    let currentDeck = this.decks[this.selectedDeck]
    currentDeck.forEach(cardSCID => {
        let card = utils.findObjectByKey(this.cards, 'ID', cardUtils.SCIDtoInstanceID(cardSCID))

        self.writeVInt(card.ID)
        self.writeVInt(card.level)
        self.writeVInt(0)
        self.writeVInt(card.xpPoints)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeBoolean(false)
        self.writeBoolean(false)
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
            self.writeBoolean(false)
            self.writeBoolean(false)
        }
    })
    self.writeVInt(this.selectedDeck)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeByte(0x7f)

    self.writeVInt(33)
    self.writeVInt(0) // CurrentTimestamp
    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(1) // EventCount
    {
        self.writeVInt(1109) // ID
        self.writeString('2v2 Button')
        self.writeVInt(8) // Type
        self.writeVInt(Math.floor(Date.now() / 1000)) // StartTime
        self.writeVInt(2147483647) // EndTime
        self.writeVInt(Math.floor(Date.now() / 1000)) // VisibleOn
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeVInt(0)
        self.writeString('2v2 Button')
        self.writeString('{\"HideTimer\":false,\"HidePopupTimer\":false}\"')
    }

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeByte(0x7f)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0) // ChallengeEvents

    self.writeVInt(1) // EventCount
    {
        self.writeVInt(1109)
    }

    self.writeVInt(2) // EventCount
    {
        self.writeVInt(2)
        self.writeString('{"ID":"CARD_RELEASE","Params":{}})')
        self.writeVInt(4)
        self.writeString('{"ID":"CLAN_CHEST","Params":{}}')
    }

    // Chest Slot
    self.writeVInt(4)
    {
        self.writeVInt(0) // DisableChests
    }

    self.writeVInt(0) // FreeChestTimer
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeBoolean(false)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0) // CrownChestCount
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(-1)
    self.writeVInt(1714640)
    self.writeVInt(1726960)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(-1)

    self.writeVInt(1) // 1 = SetNamePopup, 2 = Upgrade Card Tutorial, 3 = NameSet

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(2)
    self.writeVInt(13) // Level
    self.writeVInt(55)
    self.writeVInt(1) // OldArena

    //shop
    self.writeVInt(1) // ShopDay
    self.writeVInt(1) // Seed
    self.writeVInt(1) // DaySeen

    self.writeVInt(0) // TicksUntilTomorrow
    self.writeVInt(0)

    self.writeVInt(0)

    self.writeVInt(0) // OfferCount

    self.writeVInt(0) // Special

    for (var i = 0; i < 3; i++) {
      self.writeVInt(0)
      self.writeVInt(0)
      self.writeVInt(0)
    }

    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(0)

    self.writeVInt(0) // CardRequest?

    self.writeVInt(0)

    self.writeVInt(23)

    // Array
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeShort(-2041)

    self.writeVInt(1)
    self.writeVInt(1)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(11)
    self.writeVInt(0)

    self.writeVInt(2)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(4)
    self.writeVInt(3)
    self.writeVInt(17)
    self.writeVInt(1)

    self.writeVInt(14)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(5)
    self.writeVInt(4)
    self.writeVInt(14)
    self.writeVInt(1)

    self.writeVInt(74)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(5)
    self.writeVInt(4)
    self.writeVInt(1)
    self.writeVInt(1)

    self.writeVInt(73)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(5)
    self.writeVInt(0)

    self.writeVInt(4)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(9)
    self.writeVInt(0)

    self.writeVInt(15)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(6)
    self.writeVInt(2)

    self.writeVInt(16)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(1)
    self.writeVInt(6)
    self.writeVInt(2)

    self.writeVInt(0)

    // Missions
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(0) // NewArenasSeenCount

    self.writeVInt(0) // SessionReward = 2
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)

    self.writeVInt(7) // TrainingBattlesCompleted
  }
}

module.exports = ClientHome