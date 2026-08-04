namespace CRS.Core
{
    #region Usings

    using System;
    using System.Diagnostics;

    #endregion Usings

    internal class Performance : Stopwatch
    {
        public Performance()
        {
            this.Start();
        }

        public new TimeSpan Stop()
        {
            base.Stop();
            return this.Elapsed;
        }
    }
}