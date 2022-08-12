using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADCD_Indexed : ADCD_Instruction, IIndexed 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xA9; } }
	}
}
