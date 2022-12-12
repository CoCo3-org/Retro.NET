using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace IntegrationTests
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			StreamWriter sw = new StreamWriter(@"..\..\..\..\..\_countLog.txt", true);
			sw.WriteLine("101122\tRetro.NET\t" + System.DateTime.Now + "\tIntegration Tests");
			sw.Close();
		}
	}
}
