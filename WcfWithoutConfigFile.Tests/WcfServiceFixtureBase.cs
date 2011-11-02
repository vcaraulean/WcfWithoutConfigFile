using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using NUnit.Framework;

namespace WcfWithoutConfigFile.Tests
{
	[TestFixture]
	public abstract class WcfServiceFixtureBase<TService, TServiceImpl> where TServiceImpl : TService where TService : class
	{
		private readonly string baseAddress = "http://localhost:12345/" + typeof(TServiceImpl).Name;
		private readonly Binding binding = new BasicHttpBinding();
		private ServiceHost serviceHost;

		[SetUp]
		public void SetUp()
		{
			serviceHost = new ServiceHost(typeof(TServiceImpl));
			serviceHost.AddServiceEndpoint(typeof(TService), binding, baseAddress);

			serviceHost.Open();
		}

		[TearDown]
		public void TearDown()
		{
			if (Service != null)
			{
				var communicationObject = ((ICommunicationObject)Service);
				try
				{
					communicationObject.Close();
				}
				catch (CommunicationException)
				{
					communicationObject.Abort();
				}
				catch (TimeoutException)
				{
					communicationObject.Abort();
				}
				catch (Exception)
				{
					communicationObject.Abort();
					throw;
				}
			}
			
			if (serviceHost.State != CommunicationState.Faulted)
				serviceHost.Close();
		}

		private TService service;
		protected TService Service
		{
			get
			{
				if (service == null)
				{
					var channelFactory = new ChannelFactory<TService>(binding, baseAddress);
					service = channelFactory.CreateChannel();
				}
				return service;
			}
		}
	}
}