using System;
using System.Collections.Generic;
using System.Text;

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
