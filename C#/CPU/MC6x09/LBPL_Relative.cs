using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBPL_Relative : Instruction, IRelative 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x2A; } }

		public override string Mnemonic { get { return "LBPL"; } }
	}
}
