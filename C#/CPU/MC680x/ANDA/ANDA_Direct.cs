using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ANDA_Direct : ANDA_Instruction, IDirect
	{
		public override byte OpCode { get { return 0x94; } }
	}
}
