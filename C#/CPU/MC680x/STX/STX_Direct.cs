using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STX_Direct : STX_Instruction, IDirect
	{
		public override byte OpCode { get { return 0xDF; } }
	}
}
