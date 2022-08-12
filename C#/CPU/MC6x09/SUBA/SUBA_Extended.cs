using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SUBA_Extended : SUBA_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0xB0; } }
	}
}
