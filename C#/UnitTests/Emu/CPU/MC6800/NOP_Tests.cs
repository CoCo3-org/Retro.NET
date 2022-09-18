using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.Emu.CPU.MC6800
{
	[TestFixture]
	public class NOP_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var cpu = new global::Emu.CPU.MC680x.MC6800();

			// Test FLAGS before
			// Test Registers before
			Assert.That(cpu.REG_PC, Is.EqualTo(0));
			cpu.NOP();
			// Test FLAGS before
			// Test Registers after
			Assert.That(cpu.REG_PC, Is.EqualTo(1));
		}
	}
}
