using System;
using System.Collections.Generic;
using System.Text;

namespace Emu.CPU
{
	public class RAM : IORange
	{
		public byte[] Memory { get; private set; }

		public RAM(int lowAddress, int highAddress) 
			: base(lowAddress, highAddress)
		{
			this.Memory = new byte[highAddress - lowAddress+1];
		}

		public override int ReadByte(int address) 
		{
			return this.Memory[address - LowAddress];
		}

		public override void WriteByte(int address, int value) 
		{
			this.Memory[address - LowAddress] = (byte)value;
		}
	}
}
