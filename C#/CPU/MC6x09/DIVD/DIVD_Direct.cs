using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DIVD_Direct : DIVD_Instruction, IDirect 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x9D; } }
	}
}
