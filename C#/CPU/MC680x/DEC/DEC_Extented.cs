using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DEC_Extended : DEC_Instruction, IExtended
	{
		public override byte OpCode { get { return 0x7A; } }
	}
}
