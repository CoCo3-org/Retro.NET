using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace UnitTests
{
	using NUnit.Framework;

	[SetUpFixture]
	public class SetUpFixture
	{
		[SetUp]
		public void RunBeforeAnyTests()
		{
			StreamWriter sw = new StreamWriter(@"..\..\..\_countLog.txt", true);
			sw.WriteLine("72120\tRetro.IDE\t" + System.DateTime.Now + "\tUnit Tests");
			sw.Close();
		}
	}
}
