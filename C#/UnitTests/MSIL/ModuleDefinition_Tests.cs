using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.MSIL
{
	[TestFixture]
	public class ModuleDefinition_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::MSIL.ModuleDefinition();

			Assert.IsTrue(false);
		}
	}
}
