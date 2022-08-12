using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORCC_Immediate : Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x1A; } }

		public override string Mnemonic { get { return "ORCC"; } }
	}
}
