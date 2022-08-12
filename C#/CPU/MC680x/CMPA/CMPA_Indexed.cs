using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CMPA_Indexed : CMPA_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xA1; } }
	}
}
