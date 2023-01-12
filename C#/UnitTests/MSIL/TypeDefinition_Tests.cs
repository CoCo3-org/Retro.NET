//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
