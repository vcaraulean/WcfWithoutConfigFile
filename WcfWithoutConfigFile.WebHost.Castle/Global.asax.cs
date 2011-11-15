using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Castle.Core;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using WcfWithoutConfigFile.Service;

namespace WcfWithoutConfigFile.WebHost.Castle
{
	public class Global : System.Web.HttpApplication
	{
		private static IWindsorContainer container;
		
		protected void Application_Start(object sender, EventArgs e)
		{
			container = new WindsorContainer();

			var returnFaults = new ServiceDebugBehavior
			{
				IncludeExceptionDetailInFaults = true,
				HttpHelpPageEnabled = true
			};
			var metadata = new ServiceMetadataBehavior { HttpGetEnabled = true };

			container.Register(
				Component.For<IServiceBehavior>().Instance(returnFaults),
				Component.For<IServiceBehavior>().Instance(metadata));

			container
				.AddFacility<WcfFacility>()
				.Register(
					Component
						.For<IUploadService>()
						.ImplementedBy<UploadService>()
						.Named(typeof(IUploadService).Name)
						.LifeStyle.Is(LifestyleType.Singleton)
						.AsWcfService(new DefaultServiceModel()
						.Hosted()
						.AddEndpoints(WcfEndpoint
										.ForContract(typeof(IUploadService))
										.BoundTo(new BasicHttpBinding
										{
											Security =
												{
													Mode = BasicHttpSecurityMode.TransportCredentialOnly,
													Transport = {ClientCredentialType = HttpClientCredentialType.Windows}
												}
										})))
				);
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{
			container.Dispose();
		}
	}
}