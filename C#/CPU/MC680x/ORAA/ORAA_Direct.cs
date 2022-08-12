using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAA_Direct : ORAA_Instruction, IDirect
	{
		public override byte OpCode { get { return 0x9A; } }
	}
}
