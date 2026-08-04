using System.Threading.Tasks;
namespace UCS.Core
{
    #region Usings

    using System;
    using System.IO;

    using UCS.Logic.Enums;

    using NLog;

    #endregion Usings

    internal class Loggers : IDisposable
    {
        private static Logger Logger = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loggers"/> class.
        /// </summary>
        public Loggers()
        {
            if (Directory.Exists("Logs"))
            {
                if (Directory.GetFiles("Logs").Length > 0)
                {
                    string[] _Files = Directory.GetFiles("Logs", "*.txt", SearchOption.TopDirectoryOnly);
                    var t = Task<string>.Factory.StartNew(() => new FileInfo(_Files[0]).LastWriteTime.ToString(@"dd-MM-yyyy - HH.mm"));

                    t.Wait();
                    Directory.CreateDirectory("Logs/" + t.Result);

                    foreach (string _File in _Files)
                    {
                        FileInfo _Info = new FileInfo(_File);

                        if (File.Exists("Logs/" + t.Result + "/" + _Info.Name))
                        {
                            File.Delete("Logs/" + t.Result + "/" + _Info.Name);
                        }

                        _Info.MoveTo("Logs/" + t.Result + "/" + _Info.Name);
                    }
                }
            }

            Logger = LogManager.GetCurrentClassLogger();

            Logger.Info("Logger has been started.");
            Logger.Warn("Logger has been started.");
            Logger.Trace("Logger has been started.");
            Logger.Error("Logger has been started.");
            Logger.Fatal("Logger has been started.");
        }

        public static void Log(string _Message = "OK.", Defcon _Defcon = Defcon.DEFAULT)
        {
            switch (_Defcon)
            {
                case Defcon.DEBUG:
                {
                    Logger.Debug(_Message);
                    break;
                }

                case Defcon.INFO:
                {
                    Logger.Info(_Message);
                    break;
                }

                case Defcon.WARN:
                {
                    Logger.Warn(_Message);
                    break;
                }

                case Defcon.TRACE:
                {
                    Logger.Trace(_Message);
                    break;
                }

                case Defcon.ERROR:
                {
                    Logger.Error(_Message);
                    break;
                }

                case Defcon.FATAL:
                {
                    Logger.Fatal(_Message);
                    break;
                }

                default:
                {
                    Logger.Info(_Message);
                    break;
                }
            }
        }

        public void Dispose()
        {
            Logger = null;
        }
    }
}