using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace CRS.Core.Web
{
	// Token: 0x020000E8 RID: 232
	internal class HTTP
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00020862 File Offset: 0x0001EA62
		public HTTP(int port)
		{
			this.Initialize(port);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0002087C File Offset: 0x0001EA7C
		public HTTP()
		{
			TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
			tcpListener.Start();
			int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
			tcpListener.Stop();
			this.Initialize(port);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000208C8 File Offset: 0x0001EAC8
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x000123B6 File Offset: 0x000105B6
		public int Port
		{
			get
			{
				return this._port;
			}
			private set
			{
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000208D0 File Offset: 0x0001EAD0
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000208D8 File Offset: 0x0001EAD8
		public Dictionary<string, string> UCS { get; internal set; }

		// Token: 0x060005CA RID: 1482 RVA: 0x000208E1 File Offset: 0x0001EAE1
		public Stream GenerateStreamFromString(string s)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(s);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00020902 File Offset: 0x0001EB02
		public void Stop()
		{
			this._serverThread.Abort();
			this._listener.Stop();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002091C File Offset: 0x0001EB1C
		private static byte[] GetBytes(string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002094C File Offset: 0x0001EB4C
		private void Handler(string type)
		{
			try
			{
				if (type == "inmemclans")
				{
					this.jsonapp = Convert.ToString(ObjectManager.GetInMemoryAlliances().Count);
				}
				else if (type == "inmemplayers")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetInMemoryLevels().Count);
				}
				else if (type == "onlineplayers")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetOnlinePlayers().Count);
				}
				else if (type == "totalclients")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetConnectedClients().Count);
				}
				else if (type == "all")
				{
					JsonApi jsonApi = new JsonApi
					{
						UCS = new Dictionary<string, string>
						{
							{ "PatchingServer", "http://patch.gobelinland.fr/" },
							{ "Maintenance", "false" },
							{ "MaintenanceTimeLeft", "0" },
							{ "ClientVersion", "8.212" },
							{
								"ServerVersion",
								Assembly.GetExecutingAssembly().GetName().Version.ToString()
							},
							{
								"OnlinePlayers",
								Convert.ToString(ResourcesManager.GetOnlinePlayers().Count)
							},
							{
								"InMemoryPlayers",
								Convert.ToString(ResourcesManager.GetInMemoryLevels().Count)
							},
							{
								"InMemoryClans",
								Convert.ToString(ObjectManager.GetInMemoryAlliances().Count)
							},
							{
								"TotalConnectedClients",
								Convert.ToString(ResourcesManager.GetConnectedClients().Count)
							}
						}
					};
					this.jsonapp = JsonConvert.SerializeObject(jsonApi);
					this.mime = "application/json";
				}
				else if (type == "ram")
				{
					this.jsonapp = Performances.GetUsedMemory();
				}
				else
				{
					this.jsonapp = "OK";
				}
			}
			catch (Exception ex)
			{
				this.jsonapp = "An exception occured in UCS : \n" + ex;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00020B58 File Offset: 0x0001ED58
		private void Initialize(int port)
		{
			this._port = port;
			this._serverThread = new Thread(new ThreadStart(this.Listen));
			this._serverThread.Start();
			Console.WriteLine("[GRS]    API has been successfully started");
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00020B90 File Offset: 0x0001ED90
		private void Listen()
		{
			this._listener = new HttpListener();
			this._listener.Prefixes.Add("http://+:" + this._port + "/UCSGL/");
			this._listener.Start();
			for (;;)
			{
				try
				{
					HttpListenerContext context = this._listener.GetContext();
					this.Process(context);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00020C08 File Offset: 0x0001EE08
		private void Process(HttpListenerContext context)
		{
			IEnumerable<string> enumerable = new string[] { "inmemclans", "inmemplayers", "onlineplayers", "totalclients", "ram", "" };
			string text = context.Request.Url.AbsolutePath.Substring(7).ToLower();
			if (enumerable.Contains(text))
			{
				this.Handler(text);
				try
				{
					context.Response.ContentType = this.mime;
					context.Response.ContentEncoding = Encoding.UTF8;
					context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
					context.Response.AddHeader("Last-Modified", DateTime.UtcNow.ToString("r"));
					context.Response.AddHeader("APIVersion", "1.0a");
					byte[] array = new byte[16384];
					using (Stream stream = this.GenerateStreamFromString(this.jsonapp))
					{
						int num;
						while ((num = stream.Read(array, 0, array.Length)) > 0)
						{
							context.Response.OutputStream.Write(array, 0, num);
						}
						stream.Close();
					}
					context.Response.StatusCode = 200;
					context.Response.OutputStream.Flush();
					goto IL_0177;
				}
				catch (Exception)
				{
					context.Response.StatusCode = 500;
					goto IL_0177;
				}
			}
			context.Response.StatusCode = 404;
			IL_0177:
			context.Response.OutputStream.Close();
		}

		// Token: 0x040003E0 RID: 992
		private HttpListener _listener;

		// Token: 0x040003E1 RID: 993
		private int _port;

		// Token: 0x040003E2 RID: 994
		private Thread _serverThread;

		// Token: 0x040003E3 RID: 995
		private string jsonapp;

		// Token: 0x040003E4 RID: 996
		private string mime = "text/plain";
	}
}
