using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDB_Direct : ADDB_Instruction, IDirect
	{
		public override byte OpCode { get { return 0xDB; } }
	}
}
