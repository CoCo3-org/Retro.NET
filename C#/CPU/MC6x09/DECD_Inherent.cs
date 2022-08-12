using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DECD_Inherent : Instruction, IInherent 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x4A; } }

		public override string Mnemonic { get { return "DECD"; } }
	}
}
