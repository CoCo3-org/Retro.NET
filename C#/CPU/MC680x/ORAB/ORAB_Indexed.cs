using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAB_Indexed : ORAB_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xEA; } }
	}
}
