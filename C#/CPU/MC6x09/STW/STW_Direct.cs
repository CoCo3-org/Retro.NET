using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STW_Direct : STW_Instruction, IDirect 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x97; } }
	}
}
