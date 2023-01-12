//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
