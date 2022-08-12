using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ROR_Extended : ROR_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0x76; } }
	}
}
