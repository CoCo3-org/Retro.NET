using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STAA_Extended : STAA_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xB7; } }
	}
}
