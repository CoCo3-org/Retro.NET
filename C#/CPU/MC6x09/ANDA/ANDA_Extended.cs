using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDA_Extended : ANDA_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0xB4; } }
	}
}
