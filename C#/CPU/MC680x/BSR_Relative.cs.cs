using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BSR_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x8D; } }

		public override string Mnemonic { get { return "BSR"; } }
	}
}
