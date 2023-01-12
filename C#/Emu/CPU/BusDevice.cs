//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace Emu.CPU
{
	public class BusDevice
	{
		public int LowAddress { get; set; }
		public int HighAddress { get; set; }

		public BusDevice(int lowAddress, int highAddress = 0)
		{
			if (highAddress != 0 && lowAddress >= highAddress)
				throw new ArgumentOutOfRangeException("High Address Must be Higher!");

			this.LowAddress = lowAddress;
			this.HighAddress = highAddress;
		}

		public bool Matches(int address) 
		{
			return this.LowAddress == address;
		}

		public bool InRange(int address) 
		{
			if (address >= this.LowAddress && address <= this.HighAddress)
				return true;
			else
				return false;
		}

		public virtual int ReadByte(int address) { throw new NotImplementedException(); }
		public virtual void WriteByte(int address, int value) { throw new NotImplementedException(); }
	}
}
