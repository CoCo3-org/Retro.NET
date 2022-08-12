using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAB_Immediate : ORAB_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0xCA; } }
	}
}
