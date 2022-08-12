using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CPX_Extended : CPX_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xBC; } }
	}
}
