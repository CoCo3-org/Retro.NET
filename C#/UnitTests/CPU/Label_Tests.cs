using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class Label_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var instruction = new global::CPU.Instruction(module);
			var label = new global::CPU.Label(instruction);

			Assert.IsNull(label.Text);
			Assert.That(label.CodeLine, Is.EqualTo(instruction));
		}
	}
}
