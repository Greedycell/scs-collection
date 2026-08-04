namespace CRS.Logic.Slots
{
    using System.Collections.Generic;

    using CRS.Core.Settings;
    using CRS.Logic.Slots.Items;

    public class Resources : List<Resource>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Resources"/> class.
        /// </summary>
        public Resources()
        {
            this.Initialize();
        }

        /// <summary>
        /// Add the specified resource.
        /// </summary>
        /// <param name="_Data">The data.</param>
        /// <param name="_Value">The value.</param>
        public void Set(int _Data, int _Value)
        {
            Resource _Resource = new Resource(_Data, _Value);

            if (this.Contains(_Resource))
            {
                int _Index = this.FindIndex(Resource => Resource == _Resource);

                if (_Index > -1)
                {
                    this[_Index].Value = _Resource.Value;
                }
                else
                {
                    this.Add(_Resource);
                }
            }
            else
            {
                this.Add(_Resource);
            }
        }

        /// <summary>
        /// Add the specified resource.
        /// </summary>
        /// <param name="_Data">The data.</param>
        /// <param name="_Value">The value.</param>
        public void Set(Enums.Resource _Data, int _Value)
        {
            Resource _Resource = new Resource((int)_Data, _Value);

            if (this.Contains(_Resource))
            {
                int _Index = this.FindIndex(Resource => Resource == _Resource);

                if (_Index > -1)
                {
                    this[_Index].Value = _Resource.Value;
                }
                else
                {
                    this.Add(_Resource);
                }
            }
            else
            {
                this.Add(_Resource);
            }
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public void Initialize()
        {
            this.Set(Enums.Resource.GEMS, Settings.StartingGems);
            this.Set(Enums.Resource.GOLD, Settings.StartingGold);
            this.Set(Enums.Resource.CHEST_INDEX, 0);
            this.Set(Enums.Resource.CHEST_COUNT, 0);
            this.Set(Enums.Resource.FREE_GOLD, 0);
            this.Set(Enums.Resource.MAX_TROPHIES, Settings.StartingTrophies);
            this.Set(Enums.Resource.CARD_COUNT, Settings.StartingCards);
            this.Set(Enums.Resource.DONATIONS, 0);
            this.Set(Enums.Resource.REWARD_GOLD, 0);
            this.Set(Enums.Resource.REWARD_COUNT, 0);
            this.Set(Enums.Resource.SHOP_DAY_COUNT, 0);
        }
    }
}
