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
	public class RAM_Tests
	{
		[Test]
		public void Constructor_Defaults() 
		{
			var ram = new global::Emu.CPU.RAM(0x0000, 0x3FFF);
			Assert.That(ram.Memory.Length, Is.EqualTo(0x4000));
		}

		[Test]
		public void Read_Bytes_Default_Value_Is_Zero() 
		{
			var ram = new global::Emu.CPU.RAM(0x4000, 0x4FFF);

			Assert.That(ram.ReadByte(0x4000), Is.EqualTo(0));
			Assert.That(ram.ReadByte(0x4FFF), Is.EqualTo(0));
		}

		[Test]
		public void Write_Then_Read_Byte() 
		{
			var ram = new global::Emu.CPU.RAM(0x8000, 0x9FFF);

			ram.WriteByte(0x8000, 0xFF);
			ram.WriteByte(0x9FFF, 0x8F);

			Assert.That(ram.ReadByte(0x8000), Is.EqualTo(0xFF));
			Assert.That(ram.ReadByte(0x9FFF), Is.EqualTo(0x8F));
		}
	}
}
