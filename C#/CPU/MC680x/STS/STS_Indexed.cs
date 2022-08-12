using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STS_Indexed : STS_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xAF; } }
	}
}
