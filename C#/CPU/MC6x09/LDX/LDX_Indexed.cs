using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDX_Indexed : LDX_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xAE; } }
	}
}
