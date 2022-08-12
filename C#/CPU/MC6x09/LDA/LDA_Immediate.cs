using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDA_Immediate : LDA_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x86; } }
	}
}
