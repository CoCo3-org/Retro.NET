using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDA_Extended : ADDA_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xBB; } }
	}
}
