//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace Emu.CPU
{
	public class ROM : IORange
	{
		public byte[] Memory { get; private set; }

		public ROM(int lowAddress, int highAddress)
			: base(lowAddress, highAddress)
		{
			// this.Memory = new byte[highAddress - lowAddress + 1];
		}

		public void SetROM(byte[] bytes)
		{
			if (this.HighAddress - this.LowAddress + 1 != bytes.Length)
				throw new Exception("bytes length must be " + (this.HighAddress - this.LowAddress + 1));

			this.Memory = bytes;
		}

		public override int ReadByte(int address)
		{
			return this.Memory[address - LowAddress];
		}

		public override void WriteByte(int address, int value)
		{
			// should I check if in RANGE??
		}
	}
}
