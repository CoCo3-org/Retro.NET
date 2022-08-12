using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBLS_Relative : Instruction, IRelative 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x23; } }

		public override string Mnemonic { get { return "LBLS"; } }
	}
}
