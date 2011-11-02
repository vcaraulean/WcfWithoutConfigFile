using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace WcfWithoutConfigFile.WebHost
{
	public class ServiceHostFactory<TContract> : ServiceHostFactory
	{
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new CustomServiceHost<TContract>(serviceType, baseAddresses);
		}
	}
}