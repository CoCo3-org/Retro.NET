//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class ORG_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var directive = new global::CPU.ORG_Directive(module);

			Assert.That(directive.Label, Is.EqualTo(null));
			Assert.That(directive.Address, Is.EqualTo(0));
			Assert.That(directive.Mnemonic, Is.EqualTo("ORG"));
			Assert.That(directive.Module, Is.EqualTo(module));
			Assert.That(directive.CodeBlock, Is.EqualTo(module));
			Assert.That(directive.XmlTag, Is.EqualTo("ORG"));
		}
	}
}
