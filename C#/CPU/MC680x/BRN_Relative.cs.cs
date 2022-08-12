using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BRN_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x21; } }

		public override string Mnemonic { get { return "BRN"; } }
	}
}
