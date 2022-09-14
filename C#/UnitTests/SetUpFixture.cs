using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			StreamWriter sw = new StreamWriter(@"..\..\..\..\..\_countLog.txt", true);
			sw.WriteLine("7422\tRetro.NET\t" + System.DateTime.Now + "\tUnit Tests");
			sw.Close();
		}
	}
}
