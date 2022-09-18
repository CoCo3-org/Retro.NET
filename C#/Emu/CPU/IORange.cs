using System;
using System.Collections.Generic;
using System.Text;

namespace Emu.CPU
{
	public class IORange : BusDevice
	{
		public IORange(int lowAddress, int highAddress)
			: base(lowAddress, highAddress)
		{

		}
	}
}
