using System.Threading;

namespace CRS.Sys
{
    class LoadLanguage
    {
        private void SetLanguage()
        {
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":

                    break;
                case "it-IT":

                    break;
                default:

                    break;
            }

        }
    }
}
