using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SWI2_Inherent : Instruction, IInherent 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x3F; } }

		public override string Mnemonic { get { return "SWI2"; } }
	}
}
