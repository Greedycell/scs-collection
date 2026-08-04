namespace CRS.Logic.Slots
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CRS.Extensions.List;
    using CRS.Logic.Slots.Items;

    public class Boutique : List<Shop>
    {
#pragma warning disable CS0109 // El miembro 'Boutique.Add(byte, byte, byte, byte, int, int, DateTime)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the specified card to the shop.
        /// </summary>
        /// <param name="_Index">The index.</param>
        /// <param name="_ShopType">Type of the shop.</param>
        /// <param name="_Rarity">The rarity.</param>
        /// <param name="_Type">The type.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Count">The count.</param>
        /// <param name="_EndOffer">The end offer.</param>
        public new void Add(byte _Index, byte _ShopType, byte _Rarity, byte _Type, int _ID, int _Count, DateTime _EndOffer)
#pragma warning restore CS0109 // El miembro 'Boutique.Add(byte, byte, byte, byte, int, int, DateTime)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            Shop _Shop = new Shop(_ShopType, _Rarity, _Type, _ID, _Count, _EndOffer);


            if (_EndOffer < DateTime.UtcNow)
            {
                this[_Index] = _Shop;
            }
            else if (!(_EndOffer > DateTime.UtcNow))
            {
                base.Add(_Shop);
            }
            else
            {
                this[_Index].Count += _Count;
            }
        }

        /// <summary>
        /// Encode the card.
        /// </summary>
        /// <returns>The encoded card.</returns>
        public byte[] EncodeCard()
        {
            List<byte> _Shop = new List<byte>();

            _Shop.AddVInt(this.FindAll(c => c.ShopType != 3).Count);
            foreach(Shop _Item in this.OrderBy(c => c.Rarity).ThenByDescending(c => c.ShopType))
            {
                if (_Item.ShopType == 3)
                    continue;

                _Shop.AddVInt(_Item.ShopType);
                _Shop.Add(0x82);
                _Shop.AddVInt(1);
                _Shop.AddVInt(_Item.ShopType > 3 ? 6 + _Item.Rarity : 2);

                _Shop.Add(0);
                _Shop.AddVInt(_Item.Type);
                _Shop.AddVInt(_Item.ID);
                _Shop.AddVInt(_Item.Count);

                if (_Item.ShopType > 3)
                {
                    _Shop.AddVInt((int)(_Item.EndOffer - DateTime.Now).TotalSeconds * 20 - 10);
                    _Shop.AddVInt(0);
                    _Shop.AddVInt(0);
                }
            }

            return _Shop.ToArray();
        }

        /// <summary>
        /// Encode the offer.
        /// </summary>
        /// <returns>The encoded offer.</returns>
        public byte[] EncodeOffer()
        {
            List<byte> _Offer = new List<byte>();

            _Offer.AddVInt(this.FindAll(_Shop => _Shop.ShopType == 3).Count > 0 ? 1 : 0);
            foreach(Shop _Item in this.FindAll(shop => shop.ShopType == 3))
            {
                _Offer.Add(_Item.ShopType);
                _Offer.Add(0x82);
                _Offer.Add(1);
                _Offer.AddVInt(_Item.ID);
                _Offer.AddVInt(_Item.Count > 0 ? 1 : 0);
            }

            return _Offer.ToArray();
        }
    }
}
