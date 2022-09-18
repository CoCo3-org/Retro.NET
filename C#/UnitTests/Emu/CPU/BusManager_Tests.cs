using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.Emu.CPU
{
	[TestFixture]
	public class BusManager_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var busManager = new global::Emu.CPU.BusManager();

			var ram = new global::Emu.CPU.RAM(0x0100, 0x01FF);
			busManager.AddBusDevice(ram);

			for (int i = 0; i < 0x100; i++)
				busManager.WriteByte(0x0100 + i, i);

			var rom = new global::Emu.CPU.ROM(0xFFF0, 0xFFFF);
			rom.SetROM(new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 110, 111, 112, 113, 114, 115 });
			busManager.AddBusDevice(rom);

			Assert.That(busManager.ReadByte(10), Is.EqualTo(0));

			Assert.That(busManager.ReadByte(0x0100), Is.EqualTo(0));
			Assert.That(busManager.ReadByte(0x0101), Is.EqualTo(1));
			Assert.That(busManager.ReadByte(0x01FF), Is.EqualTo(0xFF));

		}
	}
}
