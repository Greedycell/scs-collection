namespace CRS.Logic.Enums
{
    /// <summary>
    /// Login failed because..
    /// </summary>
    public enum Reason
    {
        Default = 0,
        Patch = 7,
        Update = 8,
        Maintenance = 10,
        Banned = 11,
        Pause = 12,
        Locked = 13
    }
}