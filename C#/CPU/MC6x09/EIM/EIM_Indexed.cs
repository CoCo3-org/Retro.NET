using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EIM_Indexed : EIM_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0x65; } }
	}
}
