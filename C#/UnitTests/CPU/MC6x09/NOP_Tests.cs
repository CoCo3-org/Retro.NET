using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.CPU.MC6x09
{
	[TestFixture]
	public class NOP_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var instruction = new global::CPU.MC6x09.NOP_Inherent(module);

			Assert.IsNull(instruction.Label);
			Assert.That(instruction.Address, Is.EqualTo(0));
			Assert.That(instruction.Mnemonic, Is.EqualTo("NOP"));
			Assert.That(instruction.Module, Is.EqualTo(module));
			Assert.That(instruction.CodeBlock, Is.EqualTo(module));
			Assert.That(instruction.XmlTag, Is.EqualTo("NOP"));
		}
	}
}

