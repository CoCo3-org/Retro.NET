using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAB_Direct : ORAB_Instruction, IDirect
	{
		public override byte OpCode { get { return 0xDA; } }
	}
}
