using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDAB_Immediate : LDAB_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0xC6; } }
	}
}
