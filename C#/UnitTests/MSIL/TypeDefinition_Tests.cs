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

			Assert.IsNull(type.CecilType);
			Assert.IsNotNull(type.ParentModule);
			Assert.That(module, Is.EqualTo(type.ParentModule));

			Assert.That(type.MethodDefinitions.Count, Is.EqualTo(0));
			Assert.That(type.MethodDefinitionDict.Count, Is.EqualTo(0));
		}
	}
}
