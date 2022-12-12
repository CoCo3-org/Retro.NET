using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace IntegrationTests
{
	[SetUpFixture]
	public class Do_Nothing_App
	{
		[Test]
		public void Do_It()
		{
			Assert.IsTrue(false);
		}
	}
}
