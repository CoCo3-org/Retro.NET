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
	public class Do_Nothing_App
	{
		[Test]
		public void Do_It()
		{
			Assert.IsTrue(false);
		}
	}
}
