class LogicChangeAvatarNameCommand {
  constructor() {}

  async decode () {}

  async encode (self) {
    self.writeString(self.client.name) // Name
    self.writeInt(0) // NameSet
    //console.log(self.client.name)
  }
}

module.exports = LogicChangeAvatarNameCommand