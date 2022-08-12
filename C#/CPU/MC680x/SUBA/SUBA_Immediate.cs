using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBA_Immediate : SUBA_Instruction, IImmediate
	{
		public override byte OpCode { get { return 0x80; } }
	}
}
