using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class PSHS_Immediate : Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x34; } }

		public override string Mnemonic { get { return "PSHS"; } }
	}
}
