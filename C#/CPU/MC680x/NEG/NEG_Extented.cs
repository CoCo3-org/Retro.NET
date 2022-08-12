using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class NEG_Extended : NEG_Instruction, IExtended
	{
		public override byte OpCode { get { return 0x70; } }
	}
}
