using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STX_Direct : STX_Instruction, IDirect 
	{
		public override byte OpCode { get { return 0x9F; } }
	}
}
