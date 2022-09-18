using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.Emu.CPU
{
	[TestFixture]
	public class ROM_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var rom = new global::Emu.CPU.ROM(0xFFF0, 0xFFFF);
			rom.SetROM(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

			Assert.That(rom.Memory.Length, Is.EqualTo(16));

			Assert.That(rom.ReadByte(0xFFF0), Is.EqualTo(0));
			Assert.That(rom.ReadByte(0xFFF8), Is.EqualTo(8));
			Assert.That(rom.ReadByte(0xFFFF), Is.EqualTo(15));

		}
	}
}
