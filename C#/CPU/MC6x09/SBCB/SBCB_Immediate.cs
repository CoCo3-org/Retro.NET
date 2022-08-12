using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SBCB_Immediate : SBCB_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0xC2; } }
	}
}
