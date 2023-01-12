//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

namespace Emu.CPU
{
	public class BusManager
	{
		public LinkedList<BusDevice> Devices = new LinkedList<BusDevice>();

		public virtual int ReadByte(int address) 
		{
			foreach (BusDevice device in this.Devices)
			{
				if (device.HighAddress != 0)
				{
					if (device.InRange(address))
						return device.ReadByte(address);
				}
				else
				{
					if (device.Matches(address))
						return device.ReadByte(address);
				}
			}

			return 0; // is this correct?
		}

		public virtual void WriteByte(int address, int value) 
		{
			foreach (BusDevice device in this.Devices)
			{
				if (device.HighAddress != 0)
				{
					if (device.InRange(address))
						device.WriteByte(address, value);
				}
				else
				{
					if (device.Matches(address))
						device.WriteByte(address, value);
				}
			}
		}

		public void AddBusDevice(BusDevice device)
		{
			this.Devices.AddLast(device); 
		}
	}
}
