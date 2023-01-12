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
