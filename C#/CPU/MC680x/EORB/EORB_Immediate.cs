using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class EORB_Immediate : EORB_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0xC8; } }
	}
}
