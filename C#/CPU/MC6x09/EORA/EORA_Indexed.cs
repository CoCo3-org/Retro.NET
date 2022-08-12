using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORA_Indexed : EORA_Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0xA8; } }
	}
}
