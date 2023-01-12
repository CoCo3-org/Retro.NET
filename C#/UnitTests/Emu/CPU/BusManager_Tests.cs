//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using NUnit.Framework;

namespace UnitTests.Emu.CPU
{
	[TestFixture]
	public class BusManager_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			// create BusManager
			var busManager = new global::Emu.CPU.BusManager();
			// create RAM
			var ram = new global::Emu.CPU.RAM(0x0100, 0x01FF);
			busManager.AddBusDevice(ram);
			// create ROM
			var rom = new global::Emu.CPU.ROM(0xFFF0, 0xFFFF);
			busManager.AddBusDevice(rom);

			// set RAM values
			for (int i = 0; i < 0x100; i++)
				busManager.WriteByte(0x0100 + i, i);
			// set ROM
			rom.SetROM(new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 });

			// Test RAM values
			Assert.That(busManager.ReadByte(0x0100), Is.EqualTo(0));
			Assert.That(busManager.ReadByte(0x0101), Is.EqualTo(1));
			Assert.That(busManager.ReadByte(0x0180), Is.EqualTo(0x80));
			Assert.That(busManager.ReadByte(0x01FE), Is.EqualTo(0xFE));
			Assert.That(busManager.ReadByte(0x01FF), Is.EqualTo(0xFF));

			// No device (RAM/ROM)
			busManager.WriteByte(0x80, 0x80);  // write to no device 
			Assert.That(busManager.ReadByte(0x80), Is.EqualTo(0)); // default no device = 0

			// Test ROM values
			Assert.That(busManager.ReadByte(0xFFF0), Is.EqualTo(10));
			Assert.That(busManager.ReadByte(0xFFF1), Is.EqualTo(11));
			Assert.That(busManager.ReadByte(0xFFF8), Is.EqualTo(18));
			Assert.That(busManager.ReadByte(0xFFFE), Is.EqualTo(24));
			Assert.That(busManager.ReadByte(0xFFFF), Is.EqualTo(25));
		}
	}
}
