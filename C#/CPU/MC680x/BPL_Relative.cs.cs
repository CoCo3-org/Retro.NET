using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BPL_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x2A; } }

		public override string Mnemonic { get { return "BPL"; } }
	}
}
