using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace WcfWithoutConfigFile.WebHost
{
	public class CustomServiceHost<TContract> : ServiceHost
	{
		public CustomServiceHost(Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
		}

		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();
			var mexBehavior = Description.Behaviors.Find<ServiceMetadataBehavior>();
			if (mexBehavior == null)
			{
				mexBehavior = new ServiceMetadataBehavior();
				Description.Behaviors.Add(mexBehavior);
			}
			mexBehavior.HttpGetEnabled = true;

			AddServiceEndpoint(typeof (TContract), GetDefaultBinding(), "");
		}

		/// <summary>
		/// Customize your binding here
		/// </summary>
		/// <returns></returns>
		private static Binding GetDefaultBinding()
		{
			return new BasicHttpBinding();
		}
	}
}