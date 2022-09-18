using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.Emu.CPU.MC6800
{
	[TestFixture]
	public class MC6800_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var cpu = new global::Emu.CPU.MC680x.MC6800();

			Assert.That(cpu.REG_A, Is.EqualTo(0));
			Assert.That(cpu.REG_B, Is.EqualTo(0));
			Assert.That(cpu.REG_IP, Is.EqualTo(0));
			Assert.That(cpu.REG_PC, Is.EqualTo(0));
			Assert.That(cpu.REG_SP, Is.EqualTo(0));
		}
	}
}
