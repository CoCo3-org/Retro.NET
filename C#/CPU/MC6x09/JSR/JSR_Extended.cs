using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class JSR_Extended : JSR_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0xBD; } }
	}
}
