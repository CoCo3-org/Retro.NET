using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDA_Indexed : LDA_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xA6; } }
	}
}
