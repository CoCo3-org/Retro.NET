using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class AIM_Extended : AIM_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0x72; } }
	}
}
