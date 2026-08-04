namespace CRS.Logic.Slots.Items
{
    #region Usings

    using Enums;

    #endregion Usings

    internal class Spell
    {
        public int Type;
        public Arena UnlockArena;
        public int Rarity;

        public Spell(int _Type, Arena _UnlockArena, int _Rarity)
        {
            this.Type = _Type;
            this.UnlockArena = _UnlockArena;
            this.Rarity = _Rarity;
        }
    }
}
