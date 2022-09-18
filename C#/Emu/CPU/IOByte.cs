using System;
using System.Collections.Generic;
using System.Text;

namespace Emu.CPU
{
	public class IOByte : BusDevice
	{
		public IOByte(int lowAddress, int highAddress = 0)
			: base(lowAddress, highAddress)
		{

		}
	}
}
