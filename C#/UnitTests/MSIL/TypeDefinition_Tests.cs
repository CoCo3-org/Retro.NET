using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.MSIL
{
	[TestFixture]
	public class TypeDefinition_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::MSIL.ModuleDefinition();
			var type = new global::MSIL.TypeDefinition(module);

			Assert.IsTrue(false);
		}
	}
}
