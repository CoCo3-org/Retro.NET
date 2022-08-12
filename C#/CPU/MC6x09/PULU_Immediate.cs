using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class PULU_Immediate : Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x37; } }

		public override string Mnemonic { get { return "PULU"; } }
	}
}
