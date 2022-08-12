using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CLRF_Inherent : Instruction, IInherent 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x5F; } }

		public override string Mnemonic { get { return "CLRF"; } }
	}
}
