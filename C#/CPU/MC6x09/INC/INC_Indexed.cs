using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INC_Indexed : INC_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0x6C; } }
	}
}
