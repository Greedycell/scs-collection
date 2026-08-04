namespace CRS.Core.Consoles
{
    #region Usings

    using System;
    using System.IO;
    using System.Text;

    using CRS.Core.Settings;

    #endregion Usings

    internal class Prefixed : TextWriter
    {
        public readonly TextWriter _Original = null;

        public Prefixed()
        {
            this._Original = Console.Out;
        }

        public override Encoding Encoding
        {
            get
            {
                return new ASCIIEncoding();
            }
        }

        public override void Write(string _Text)
        {
            this._Original.Write("{0}    {1}", Constants.Product, _Text);
        }

        public override void WriteLine(string _Text)
        {
            this._Original.WriteLine("{0}    {1}", Constants.Product, _Text);
        }

        public override void WriteLine()
        {
            this._Original.WriteLine();
        }
    }
}