using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class EQU_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var directive = new global::CPU.EQU_Directive(module);

			Assert.That(directive.Label, Is.EqualTo(null));
			Assert.That(directive.Address, Is.EqualTo(0));
			Assert.That(directive.Mnemonic, Is.EqualTo("EQU"));
			Assert.That(directive.Module, Is.EqualTo(module));
			Assert.That(directive.CodeBlock, Is.EqualTo(module));
			Assert.That(directive.XmlTag, Is.EqualTo("EQU"));
		}
	}
}
