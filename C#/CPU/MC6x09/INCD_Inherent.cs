using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INCD_Inherent : Instruction, IInherent 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x4C; } }

		public override string Mnemonic { get { return "INCD"; } }
	}
}
