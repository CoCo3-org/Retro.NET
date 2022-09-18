using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace UnitTests.Emu.CPU
{
	[TestFixture]
	public class CPU_Tests
	{
		[Test]
		public void Constructor_Defaults()
		{
			var cpu = new global::Emu.CPU.Cpu();

			Assert.IsNotNull(cpu.BusManager);

		}
	}
}
