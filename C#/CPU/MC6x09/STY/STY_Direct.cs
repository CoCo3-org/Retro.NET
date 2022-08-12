using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STY_Direct : STY_Instruction, IDirect 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x9F; } }
	}
}
