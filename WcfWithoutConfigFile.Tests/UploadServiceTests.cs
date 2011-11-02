using System.Text;
using NUnit.Framework;
using WcfWithoutConfigFile.Service;

namespace WcfWithoutConfigFile.Tests
{
	[TestFixture]
	public class UploadServiceTests : WcfServiceFixtureBase<IUploadService, UploadService>
	{
		[Test]
		public void When_calling_service_should_return_length_of_uploaded_block()
		{
			var result = Service.Upload(Encoding.UTF8.GetBytes("some string"));
			Assert.AreEqual("some string".Length, result);
		}
	}
}
