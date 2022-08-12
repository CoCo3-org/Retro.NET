using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLR_Extended : CLR_Instruction, IExtended
	{
		public override byte OpCode { get { return 0x7F; } }
	}
}
