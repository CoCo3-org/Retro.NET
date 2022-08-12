using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class EORB_Extended : EORB_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xF8; } }
	}
}
