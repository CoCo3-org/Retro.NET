using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BGT_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x2E; } }

		public override string Mnemonic { get { return "BGT"; } }
	}
}
