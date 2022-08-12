using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CPX_Indexed : CPX_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xAC; } }
	}
}
