using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BNE_Relative : Instruction, IRelative 
	{
		public override byte OpCode { get { return 0x26; } }

		public override string Mnemonic { get { return "BNE"; } }
	}
}
