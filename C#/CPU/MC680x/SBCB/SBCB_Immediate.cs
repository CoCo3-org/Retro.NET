using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCB_Immediate : SBCB_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0xC2; } }
	}
}
