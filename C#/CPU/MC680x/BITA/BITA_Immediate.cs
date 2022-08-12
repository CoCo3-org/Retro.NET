using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BITA_Immediate : BITA_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0x85; } }
	}
}
