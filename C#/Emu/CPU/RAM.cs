//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
