class LogicChangeAvatarNameCommand {
  constructor() {}

  async decode () {}

  async encode (self) {
    self.writeString(this.client.name) // Name
    self.writeInt(0) // NameSet
    //console.log(this.client.name)
  }
}

module.exports = LogicChangeAvatarNameCommand