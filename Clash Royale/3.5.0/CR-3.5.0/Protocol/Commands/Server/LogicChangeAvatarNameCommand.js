class LogicChangeAvatarNameCommand {
  constructor() {}

  async decode () {}

  async encode (self, Name) {
    self.writeString(Name) // Name
    self.writeInt(0) // NameSet
    console.log(Name)
  }
}

module.exports = LogicChangeAvatarNameCommand