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
	public class BusDevice_Tests
	{
		[Test]
		public void Constructor_Defaults() 
		{
			var busDevice1 = new global::Emu.CPU.BusDevice(0xFF20);

			Assert.That(busDevice1.LowAddress, Is.EqualTo(0xFF20));
			Assert.That(busDevice1.HighAddress, Is.EqualTo(0));
		}

		[Test]
		public void Constructor_High_Low_Same_Throws_Exception() 
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => { new global::Emu.CPU.BusDevice(100, 100); });
		}

		[Test]
		public void Match_and_No_Match() 
		{
			var busDevice1 = new global::Emu.CPU.BusDevice(0xFF00);

			Assert.IsTrue(busDevice1.Matches(0xFF00));
			Assert.IsFalse(busDevice1.Matches(0xFE00));
		}

		[Test]
		public void In_Range_and_Out_of_Range() 
		{
			var busDevice1 = new global::Emu.CPU.BusDevice(0xC000, 0xDFFF);

			Assert.IsTrue(busDevice1.InRange(0xC000));
			Assert.IsTrue(busDevice1.InRange(0xD000));
			Assert.IsTrue(busDevice1.InRange(0xDFFF));
			
			Assert.IsFalse(busDevice1.InRange(0xBFFF));
			Assert.IsFalse(busDevice1.InRange(0xE000));
			Assert.IsFalse(busDevice1.InRange(0x0));
			Assert.IsFalse(busDevice1.InRange(-1));

		}

	}
}
