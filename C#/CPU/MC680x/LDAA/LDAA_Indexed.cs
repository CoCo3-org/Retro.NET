using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDAA_Indexed : LDAA_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xA6; } }
	}
}
