using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDAB_Indexed : LDAB_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xE6; } }
	}
}
