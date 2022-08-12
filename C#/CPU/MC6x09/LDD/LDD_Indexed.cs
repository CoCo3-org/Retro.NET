using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDD_Indexed : LDD_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xEC; } }
	}
}
