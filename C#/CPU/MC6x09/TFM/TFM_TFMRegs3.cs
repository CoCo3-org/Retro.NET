using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TFM_TFMRegs3 : TFM_Instruction, ITFMRegs 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x3A; } }
	}
}
