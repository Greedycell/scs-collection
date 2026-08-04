const LogicMapNode = require('./LogicMapNode')

class LogicMapRegion {
  async encode (self) {
    self.writeBoolean(false)

    /*if ( result == (_DWORD *)1 )
    {
        result = *((_DWORD **)this + 1);
        v5 = result[2];
        if ( v5 >= 1 )
        {
        v6 = v5 - 1;
        for ( i = 0; ; ++i )
        {*/
            //new LogicMapNode().encode(self)
            /*if ( v6 == i )
            break;
            result = *((_DWORD **)this + 1);
        }
        }
    }*/
  }
}

module.exports = LogicMapRegion