namespace CRS.Logic.Slots
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CRS.Extensions.List;
    using CRS.Logic.Slots.Items;

    public class Chests : List<Chest>
    {
#pragma warning disable CS0109 // El miembro 'Chests.Add(byte, int, bool, DateTime, bool)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the chest to the specified emplacement.
        /// </summary>
        /// <param name="_Emplacement">The emplacement.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="Unlock">if set to <c>true</c> [unlock].</param>
        /// <param name="UnlockTime">The unlock time.</param>
        /// <param name="_isNew">if set to <c>true</c> [is new].</param>
        public new void Add(byte _Emplacement, int _ID, bool Unlock, DateTime UnlockTime, bool _isNew)
#pragma warning restore CS0109 // El miembro 'Chests.Add(byte, int, bool, DateTime, bool)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            Chest _Chest = new Chest(_Emplacement, _ID, Unlock, UnlockTime, _isNew);

            if (this.Contains(_Chest))
            {
                int _Index = this.FindIndex(chest => chest == _Chest);

                if (_Index > -1)
                {
                    this[_Index].Unlock = Unlock;
                    this[_Index].UnlockTime = UnlockTime;
                }
                else
                {
                    base.Add(_Chest);
                }
            }
            else
            {
                base.Add(_Chest);
            }
        }

        public byte[] Encode()
        {
            List<byte> _Packet = new List<byte>();
            _Packet.AddVInt(this.Count > 0 ? 1 : 0);
            foreach (Chest _Chest in this.OrderBy(_Chest => _Chest.Emplacement))
            {
                _Packet.Add(0x13);
                _Packet.AddVInt(_Chest.ID);
                _Packet.AddVInt(_Chest.Unlock ? 1 : 0);
                _Packet.AddVInt((int) (_Chest.UnlockTime - DateTime.UtcNow).TotalSeconds);
                _Packet.Add(0x85);
                _Packet.Add(0x3D);
                _Packet.Add(0x01);
                _Packet.AddVInt(_Chest.Emplacement);
                _Packet.Add(0x00);
                _Packet.AddVInt(2);
            }

            return _Packet.ToArray();
        }
    }
}
