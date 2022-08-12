using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCB_Extended : SBCB_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xF2; } }
	}
}
