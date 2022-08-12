using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TIM_Direct : TIM_Instruction, IDirect 
	{
		public override byte OpCode { get { return 0x0B; } }
	}
}
