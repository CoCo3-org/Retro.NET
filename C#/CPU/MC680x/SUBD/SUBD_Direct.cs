using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBD_Direct : SUBD_Instruction, IDirect
	{
		public override byte OpCode { get { return 0x93; } }
	}
}
