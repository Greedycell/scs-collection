using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace UCS.Core
{
	// Token: 0x020000B5 RID: 181
	internal class HTTP
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x000162A0 File Offset: 0x000144A0
		public HTTP(int port)
		{
			this.Initialize(port);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000162BC File Offset: 0x000144BC
		public HTTP()
		{
			TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
			tcpListener.Start();
			int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
			tcpListener.Stop();
			this.Initialize(port);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00016308 File Offset: 0x00014508
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00016310 File Offset: 0x00014510
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00016318 File Offset: 0x00014518
		public Dictionary<string, string> UCS { get; internal set; }

		// Token: 0x06000557 RID: 1367 RVA: 0x00016321 File Offset: 0x00014521
		public void Stop()
		{
			this._serverThread.Abort();
			this._listener.Stop();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001633C File Offset: 0x0001453C
		private void Listen()
		{
			this._listener = new HttpListener();
			this._listener.Prefixes.Add(string.Concat(new object[]
			{
				"http://+:",
				this._port,
				"/",
				ConfigurationManager.AppSettings["ApiKey"],
				"/"
			}));
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

		// Token: 0x06000559 RID: 1369 RVA: 0x000163DC File Offset: 0x000145DC
		private static byte[] GetBytes(string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00016409 File Offset: 0x00014609
		public Stream GenerateStreamFromString(string s)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(s);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001642C File Offset: 0x0001462C
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
							{
								"PatchingServer",
								ConfigurationManager.AppSettings["patchingServer"]
							},
							{
								"Maintenance",
								ConfigurationManager.AppSettings["maintenanceMode"]
							},
							{
								"MaintenanceTimeLeft",
								ConfigurationManager.AppSettings["maintenanceTimeLeft"]
							},
							{
								"ClientVersion",
								ConfigurationManager.AppSettings["clientVersion"]
							},
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

		// Token: 0x0600055C RID: 1372 RVA: 0x00016660 File Offset: 0x00014860
		private void Process(HttpListenerContext context)
		{
			IEnumerable<string> enumerable = new string[]
			{
				"inmemclans",
				"inmemplayers",
				"onlineplayers",
				"totalclients",
				"ram",
				string.Empty
			};
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

		// Token: 0x0600055D RID: 1373 RVA: 0x00016810 File Offset: 0x00014A10
		private void Initialize(int port)
		{
			this._port = port;
			this._serverThread = new Thread(new ThreadStart(this.Listen));
			this._serverThread.Start();
			Console.WriteLine("[UCS]    API has been successfully started");
		}

		// Token: 0x04000314 RID: 788
		private HttpListener _listener;

		// Token: 0x04000315 RID: 789
		private int _port;

		// Token: 0x04000316 RID: 790
		private Thread _serverThread;

		// Token: 0x04000317 RID: 791
		private string jsonapp;

		// Token: 0x04000318 RID: 792
		private string mime = "text/plain";
	}
}
