using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCB_Extended : ADCB_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xF9; } }
	}
}
