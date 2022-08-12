using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class EORB_Direct : EORB_Instruction, IDirect
	{
		public override byte OpCode { get { return 0xD8; } }
	}
}
