using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BITA_Immediate : BITA_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x85; } }
	}
}
