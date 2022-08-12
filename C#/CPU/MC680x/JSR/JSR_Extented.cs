using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class JSR_Extended : JSR_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xBD; } }
	}
}
