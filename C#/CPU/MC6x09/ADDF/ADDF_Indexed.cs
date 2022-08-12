using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDF_Indexed : ADDF_Instruction, IIndexed 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xEB; } }
	}
}
