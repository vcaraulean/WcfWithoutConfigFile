using System;
using System.ServiceModel;

namespace WcfWithoutConfigFile.Service
{
	[ServiceContract]
	public interface IUploadService
	{
		[OperationContract]
		int Upload(byte[] data);

		[OperationContract]
		string GetCurrentWindowsIdentityName();
	}

	public class UploadService : IUploadService
	{
		public int Upload(byte[] data)
		{
			Console.WriteLine("Uploaded {0} bytes.", data.Length);
			return data.Length;
		}

		public string GetCurrentWindowsIdentityName()
		{
			return OperationContext.Current.ServiceSecurityContext.WindowsIdentity.Name;
		}
	}
}
