using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STAA_Indexed : STAA_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xA7; } }
	}
}
