using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STAB_Indexed : STAB_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xE7; } }
	}
}
