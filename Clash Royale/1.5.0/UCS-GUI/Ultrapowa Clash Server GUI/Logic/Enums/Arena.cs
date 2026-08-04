namespace CRS.Logic.Enums
{
    /// <summary>
    /// <see cref="Arena"/> is an enum containing all available arena in Clash Royale.
    /// </summary>
    public enum Arena
    {
        TRAINING_CAMP   = 0,
        ARENA_1         = 1,
        ARENA_2         = 2,
        ARENA_3         = 3,
        ARENA_4         = 4,
        ARENA_5         = 5,
        ARENA_6         = 6,
        ARENA_L         = 7,
        ARENA_7         = 8,
        ARENA_8         = 9,
    }

    public class String_To_Arena_ID
    {
        public static Arena GetArenaID(string arena)
        {
            switch (arena)
            {
                case "TrainingCamp": return Arena.TRAINING_CAMP;
                case "Arena1": return Arena.ARENA_1;
                case "Arena2": return Arena.ARENA_2;
                case "Arena3": return Arena.ARENA_3;
                case "Arena4": return Arena.ARENA_4;
                case "Arena5": return Arena.ARENA_5;
                case "Arena6": return Arena.ARENA_6;
                case "Arena7": return Arena.ARENA_7;
                case "Arena8": return Arena.ARENA_8;
                case "ArenaL": return Arena.ARENA_L;
                default      : return Arena.TRAINING_CAMP;
            }
        }
    }
}
