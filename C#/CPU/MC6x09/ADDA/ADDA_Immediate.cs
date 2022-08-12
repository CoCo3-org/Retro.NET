using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDA_Immediate : ADDA_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x8B; } }
	}
}
