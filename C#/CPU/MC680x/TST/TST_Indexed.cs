using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TST_Indexed : TST_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0x6D; } }
	}
}
