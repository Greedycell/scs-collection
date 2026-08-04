class ClientAvatar {
  async encode (self) {
    self.writeVInt(0) // HighID
    self.writeVInt(1) // LowID
    self.writeVInt(0) // HighID
    self.writeVInt(1) // LowID
    self.writeVInt(0) // HighID
    self.writeVInt(1) // LowID
    self.writeString('You') // Name

    self.writeVInt(0)
    self.writeVInt(2)
    self.writeVInt(0) // Trophies



    //self.writeHex([0, 0, 0, 0, 0, 0, 0, 0]));
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);

    self.writeVInt(41)




    //self.writeHex([0, 0, 0, 0, 0]));
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);
    self.writeByte(0);

    self.writeVInt(8) //Resources Prefix???
    self.writeVInt(14) //Resources Count

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(1)//SCID Resource ID (Gold)
    self.writeVInt(1000000000) // Gold

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(5)//SCID Resource ID (Gold)
    self.writeVInt(1000000000) // Gold

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(29)//SCID Resource ID (GameMode)
    self.writeVInt(72000006)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(13)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(33)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(34)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(14)//SCID Resource ID (Unknown)
    self.writeVInt(5)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(38)//SCID Resource ID (Unknown)
    self.writeVInt(0)
    
    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(3)//SCID Resource ID (Unknown)
    self.writeVInt(2)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(37)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(2)//SCID Resource ID (Unknown)
    self.writeVInt(6)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(16)//SCID Resource ID (Unknown)
    self.writeVInt(1)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(4)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(5)//SCID Resource Type (Resources)
    self.writeVInt(28)//SCID Resource ID (Unknown)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(6)
    self.writeVInt(60)

    self.writeVInt(7)
    self.writeVInt(9)
    self.writeVInt(60)

    self.writeVInt(8)
    self.writeVInt(9)
    self.writeVInt(60)

    self.writeVInt(9)
    self.writeVInt(9)
    self.writeVInt(60)

    self.writeVInt(4)
    self.writeVInt(1)
    self.writeVInt(60)

    self.writeVInt(5)
    self.writeVInt(1)
    self.writeVInt(60)

    self.writeVInt(6)
    self.writeVInt(1)
    self.writeVInt(0)

    self.writeVInt(6)
    self.writeVInt(5)
    self.writeVInt(11)
    self.writeVInt(41)
    self.writeVInt(5)
    self.writeVInt(8)
    self.writeVInt(9)
    self.writeVInt(5)
    self.writeVInt(27)
    self.writeVInt(1)
    self.writeVInt(5)
    self.writeVInt(7)
    self.writeVInt(1)
    self.writeVInt(5)
    self.writeVInt(6)
    self.writeVInt(30)
    self.writeVInt(5)

    self.writeVInt(9)
    self.writeVInt(26000003)

    self.writeVInt(9)
    self.writeVInt(26)
    self.writeVInt(3)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(1)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(13)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(28)
    self.writeVInt(1)

    self.writeVInt(0)
    self.writeVInt(28)
    self.writeVInt(0)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(12)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(18)

    self.writeVInt(0)
    self.writeVInt(26)
    self.writeVInt(14)


    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1000000000)//Diamonds
    self.writeVInt(1000000000)//Free Diamonds

    self.writeVInt(0)//XPlevel
    self.writeVInt(13)//Level
    self.writeVInt(0)//Unk

    self.writeVInt(1)//ChangeNameCount
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(5)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(0)
    self.writeVInt(1)
    self.writeVInt(0)
    self.writeVInt(Date.now() / 1000 | 0)
    self.writeVInt(Date.now() / 1000 | 0)
    self.writeVInt(0)//Tick
  }
}

module.exports = ClientAvatar