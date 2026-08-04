using UCS.Core.Network;

namespace UCS.Core
{
    using GameFiles;
    #region Usings

    using System;

    using UCS.Core.API;
    using UCS.Core.Consoles;
    using UCS.Core.Database;
    //using UCS.Core.Network.Ancient;
    using UCS.Core.Network.UDP;
    using UCS.Files;
    using UCS.Packets;

    #endregion Usings

    /// <summary>
    /// This class load and initialize all required classes.
    /// </summary>
    internal class Loader
    {
        private Launcher _Launcher                  = null;

        private MessageFactory _MessageFactory     = null;
        private Command_Factory _CommandFactory     = null;
        private ResourcesManager _ResourcesManager  = null;
        private Loggers _Loggers                    = null;

        //private Redis _Redis                        = null;
        //private MySQL_Backup _MySQL                        = null;

        //private EventsHandler _EventsHandler        = null;
        //private Parser _Parser                      = null;

        //private FingerPrint _Fingerprint            = null;
        //private CSV _CSV                            = null;
        private Home _Home                          = null;

        private Checker _Checker                    = null;

        //private WebAPI _WebAPI                      = null;
        private Gateway _Gateway                    = null;
        //private UDP _Server                         = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loader"/> class.
        /// </summary>
        public Loader()
        {
            Performance Perf        = new Performance();

            // Safe Start..
            this._Launcher          = new Launcher();

            // Core
            this._MessageFactory    = new MessageFactory();
            this._CommandFactory    = new Command_Factory();
            this._ResourcesManager  = new ResourcesManager();
            this._Loggers           = new Loggers();

            // Databases
            //this._Redis             = new Redis();
             //this._MySQL             = new MySQL_Backup();

            // User-Side
            //this._EventsHandler     = new EventsHandler();
            //this._Parser            = new Parser();

            // Files
            //this._Fingerprint       = new Fingerprint();
            //this._CSV               = new CSV();
            this._Home              = new Home();

            // Optimizations
            this._Checker           = new Checker();

            // Network
            // this._WebAPI            = new WebAPI();
            this._Gateway           = new Gateway();
            // this._Server            = new UDP();

            // Test
            //new Test();

            Console.WriteLine($"Servidor iniciado en {Perf.Stop().Milliseconds} Milisegundos.\n");
        }
    }
}
