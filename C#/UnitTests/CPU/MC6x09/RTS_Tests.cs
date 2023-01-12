//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using NUnit.Framework;

namespace UnitTests.CPU.MC6x09
{
	[TestFixture]
	public class RTS_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var instruction = new global::CPU.MC6x09.RTS_Inherent(module);

			Assert.IsNull(instruction.Label);
			Assert.That(instruction.Address, Is.EqualTo(0));
			Assert.That(instruction.Mnemonic, Is.EqualTo("RTS"));
			Assert.That(instruction.Module, Is.EqualTo(module));
			Assert.That(instruction.CodeBlock, Is.EqualTo(module));
			Assert.That(instruction.XmlTag, Is.EqualTo("RTS"));
		}
	}
}
