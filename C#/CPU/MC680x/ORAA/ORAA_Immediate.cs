using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAA_Immediate : ORAA_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0x8A; } }
	}
}
