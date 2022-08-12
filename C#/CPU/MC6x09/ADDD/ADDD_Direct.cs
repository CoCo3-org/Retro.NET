using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDD_Direct : ADDD_Instruction, IDirect 
	{
		public override byte OpCode { get { return 0xD3; } }
	}
}
