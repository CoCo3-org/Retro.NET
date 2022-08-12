using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORB_Direct : ORB_Instruction, IDirect 
	{
		public override byte OpCode { get { return 0xDA; } }
	}
}
