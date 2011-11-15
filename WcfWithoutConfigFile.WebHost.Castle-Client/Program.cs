using System;
using WcfWithoutConfigFile.WebHost.Castle_Client.RemoteServices;

namespace WcfWithoutConfigFile.WebHost.Castle_Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new UploadServiceClient();
			var bytesProcessed = client.Upload(new byte[] {1, 2, 3, 4});
			Console.WriteLine("Bytes processed: " + bytesProcessed);

			var windowsIdentity = client.GetCurrentWindowsIdentityName();
			Console.WriteLine("Current Windows Identity as detected by server: " + windowsIdentity);
			Console.ReadLine();
		}
	}
}
