using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using NStack;
using Terminal.Gui;

namespace EncryptedUsb
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		public static void Exit()
		{
			Environment.Exit(0);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		public static void Crypt(string datadir, string conf_file_name, string password)
		{
			Program.<Crypt>d__11 <Crypt>d__;
			<Crypt>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Crypt>d__.datadir = datadir;
			<Crypt>d__.conf_file_name = conf_file_name;
			<Crypt>d__.password = password;
			<Crypt>d__.<>1__state = -1;
			<Crypt>d__.<>t__builder.Start<Program.<Crypt>d__11>(ref <Crypt>d__);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002097 File Offset: 0x00000297
		public static List<string> Decrypt_To_Ram()
		{
			return new List<string>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000209E File Offset: 0x0000029E
		private static int ord(char a)
		{
			return (int)a;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A1 File Offset: 0x000002A1
		private static char chr(int a)
		{
			return (char)a;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A8 File Offset: 0x000002A8
		public static string EncryptFile(string name, string key)
		{
			string data = Convert.ToBase64String(File.ReadAllBytes(name));
			string separator = "&";
			int charcount = 0;
			int length2 = 0;
			string payload = "";
			foreach (char c in key)
			{
				charcount += Program.ord(c);
			}
			int lenght = data.Length;
			foreach (char c2 in data)
			{
				length2++;
				if (length2 < lenght)
				{
					payload = payload + (Program.ord(c2) * charcount).ToString() + separator;
				}
				else
				{
					payload += (Program.ord(c2) * charcount).ToString();
				}
			}
			return payload;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002174 File Offset: 0x00000374
		public static string DecryptFile(string name, string key)
		{
			char separator = '&';
			int charcount = 0;
			string data = File.ReadAllText(name);
			string decrypted = "";
			foreach (char c in key.ToCharArray())
			{
				charcount += Program.ord(c);
			}
			foreach (string piece in data.Split(separator, StringSplitOptions.None))
			{
				decrypted += Program.chr(int.Parse(piece) / charcount).ToString();
			}
			return Encoding.ASCII.GetString(Convert.FromBase64String(decrypted));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002214 File Offset: 0x00000414
		public static void Encrypt(string key)
		{
			Program.<Encrypt>d__17 <Encrypt>d__;
			<Encrypt>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Encrypt>d__.key = key;
			<Encrypt>d__.<>1__state = -1;
			<Encrypt>d__.<>t__builder.Start<Program.<Encrypt>d__17>(ref <Encrypt>d__);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		public static void Decrypt(string key)
		{
			Program.<Decrypt>d__18 <Decrypt>d__;
			<Decrypt>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Decrypt>d__.key = key;
			<Decrypt>d__.<>1__state = -1;
			<Decrypt>d__.<>t__builder.Start<Program.<Decrypt>d__18>(ref <Decrypt>d__);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002284 File Offset: 0x00000484
		private static void Main(string[] args)
		{
			try
			{
				Console.SetWindowSize(30, 10);
				Application.Init(null, null);
				Application.CurrentView.Width = 30;
				Application.CurrentView.Height = 10;
				Toplevel top = Application.Top;
				Window win = new Window("SerialCrypt")
				{
					X = 0,
					Y = 1,
					Width = 30,
					Height = 10
				};
				top.Add(win);
				MenuBarItem[] array = new MenuBarItem[1];
				int num = 0;
				ustring ustring = "_Menu";
				MenuItem[] array2 = new MenuItem[1];
				array2[0] = new MenuItem("_Exit", "", delegate()
				{
					Program.Exit();
				}, null, null);
				array[num] = new MenuBarItem(ustring, array2, null);
				MenuBar menu = new MenuBar(array);
				top.Add(menu);
				win.Add(new View[]
				{
					Program.Label1 = new Label(),
					Program.ComboBox1 = new ComboBox(),
					Program.TextField1 = new TextField(3, 3, 23, "Password"),
					Program.Button1 = new Button(9, 6, "Crypt")
				});
				Program.Label1.Y = 4;
				Program.Label1.X = 3;
				Program.Label1.Width = 23;
				Program.ComboBox1.X = 3;
				Program.ComboBox1.Y = 1;
				Program.ComboBox1.Width = 23;
				Program.Button1.Clicked += delegate()
				{
					Program.Crypt(Program.ComboBox1.Text.ToString(), Program.config_name, Program.TextField1.Text.ToString());
				};
				Program.Label1.Text = "Init";
				List<ustring> items = new List<ustring>();
				foreach (DriveInfo drive in DriveInfo.GetDrives())
				{
					try
					{
						if (drive.DriveType == DriveType.Removable)
						{
							items.Add(drive.Name);
						}
					}
					catch (Exception)
					{
					}
				}
				Program.ComboBox1.SetSource(items);
				Application.Run();
			}
			catch (Exception e)
			{
				Program.Label1.Text = e.ToString();
				Thread.Sleep(5000);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ComboBox ComboBox1;

		// Token: 0x04000002 RID: 2
		private static Button Button1;

		// Token: 0x04000003 RID: 3
		private static string data_dir;

		// Token: 0x04000004 RID: 4
		private static Button Button2;

		// Token: 0x04000005 RID: 5
		private static TextField TextField1;

		// Token: 0x04000006 RID: 6
		public static string version = "1";

		// Token: 0x04000007 RID: 7
		public static string config_name = "config";

		// Token: 0x04000008 RID: 8
		public static string extension = ".crypted";

		// Token: 0x04000009 RID: 9
		public static bool encrypted;

		// Token: 0x0400000A RID: 10
		private static Label Label1;
	}
}
