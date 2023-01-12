//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
