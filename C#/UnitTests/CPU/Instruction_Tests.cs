using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.CPU
{
	[TestFixture]
	public class Instruction_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var module = new global::CPU.Module();
			var instruction = new global::CPU.Instruction(module);

			Assert.That(instruction.PreByte, Is.EqualTo(null));
			Assert.Throws<NotImplementedException>(() => { byte x = instruction.OpCode; });
			// Assert.Throws<NotImplementedException>(delegate { byte x = instruction.OpCode; });
			Assert.That(instruction.Desc, Is.EqualTo(null));
			Assert.That(instruction.Category, Is.EqualTo(null));
		}
	}
}
