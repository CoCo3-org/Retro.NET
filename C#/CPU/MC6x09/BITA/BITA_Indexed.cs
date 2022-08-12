using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BITA_Indexed : BITA_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xA5; } }
	}
}
