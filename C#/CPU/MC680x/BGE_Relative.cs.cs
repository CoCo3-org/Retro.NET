using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BGE_Relative : Instruction, IRelative
	{
		public override byte OpCode { get { return 0x2C; } }

		public override string Mnemonic { get { return "BGE"; } }
	}
}
