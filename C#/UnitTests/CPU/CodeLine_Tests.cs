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
	public class CodeLine_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var codeLine = new global::CPU.CodeLine(module);

			Assert.That(codeLine.Label, Is.EqualTo(null));
			Assert.That(codeLine.Address, Is.EqualTo(0));
			Assert.That(codeLine.Mnemonic, Is.EqualTo(null));
			Assert.That(codeLine.Module, Is.EqualTo(module));
			Assert.That(codeLine.CodeBlock, Is.EqualTo(module));
			Assert.That(codeLine.XmlTag, Is.EqualTo(null));
		}
	}
}
