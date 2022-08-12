using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBLT_Relative : Instruction, IRelative 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x2D; } }

		public override string Mnemonic { get { return "LBLT"; } }
	}
}
