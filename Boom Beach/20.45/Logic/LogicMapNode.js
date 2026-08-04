class LogicMapNode {
  async encode (self) {
    self.writeInt(0)
    self.writeInt(0)
    self.writeString('')
    /*NumResources = LogicResourceBundle::getNumResources(*((LogicResourceBundle **)this + 3));
    if ( NumResources >= 1 )
    {
        do
        {
        v6 = *((LogicResourceBundle **)this + 3);
        Int = ByteStream::readInt(a2);
        LogicResourceBundle::setAmount(v6, v4++, Int);
        }
        while ( NumResources != v4 );
    }*/
    self.writeInt(0)
    self.writeInt(0)
    self.writeBoolean(false)
    self.writeInt(0)
  }
}

module.exports = LogicMapNode