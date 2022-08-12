using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDD_Immediate : ADDD_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0xC3; } }
	}
}
