using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDB_Immediate : ADDB_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0xCB; } }
	}
}
