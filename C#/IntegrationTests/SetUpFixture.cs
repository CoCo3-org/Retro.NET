//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
