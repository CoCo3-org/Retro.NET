using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDA_Indexed : ANDA_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xA4; } }
	}
}
