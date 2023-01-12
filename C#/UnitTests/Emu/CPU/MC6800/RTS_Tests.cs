//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using NUnit.Framework;

namespace UnitTests.Emu.CPU.MC6800
{
	[TestFixture]
	public class RTS_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var cpu = new global::Emu.CPU.MC680x.MC6800();
			var ram = new global::Emu.CPU.RAM(1, 5);
			ram.WriteByte(1, 0x10);
			ram.WriteByte(2, 0x11);
			ram.WriteByte(3, 0x12);
			ram.WriteByte(4, 0x13);
			cpu.BusManager.AddBusDevice(ram);

			// Test FLAGS before
			// Test Registers before
			Assert.That(cpu.REG_SP, Is.EqualTo(0));
			Assert.That(cpu.REG_PC, Is.EqualTo(0));
			cpu.RTS();
			// Test FLAGS before
			// 

			// Test Registers after
			Assert.That(cpu.REG_SP, Is.EqualTo(2));
			Assert.That(cpu.REG_PC, Is.EqualTo(0x1011));

			cpu.RTS();
			Assert.That(cpu.REG_SP, Is.EqualTo(4));
			Assert.That(cpu.REG_PC, Is.EqualTo(0x1213));
		}
	}
}
