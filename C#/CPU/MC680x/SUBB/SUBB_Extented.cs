using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBB_Extended : SUBB_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xF0; } }
	}
}
