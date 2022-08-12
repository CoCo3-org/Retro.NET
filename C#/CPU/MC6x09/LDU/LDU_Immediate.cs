using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDU_Immediate : LDU_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0xCE; } }
	}
}
