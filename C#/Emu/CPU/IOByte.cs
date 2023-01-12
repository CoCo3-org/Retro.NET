//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
