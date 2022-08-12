using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SUBB_Indexed : SUBB_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xE0; } }
	}
}
