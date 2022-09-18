using System;
using System.Collections.Generic;
using System.Text;

namespace Emu.CPU
{
	public class Cpu
	{
		public BusManager BusManager { get; set; } = new BusManager();
		
		public Cpu()
		{
		}
	}
}
