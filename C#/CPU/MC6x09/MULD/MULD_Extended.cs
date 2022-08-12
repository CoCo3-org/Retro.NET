using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class MULD_Extended : MULD_Instruction, IExtended 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xBF; } }
	}
}
