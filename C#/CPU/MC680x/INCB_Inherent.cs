using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class INCB_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x5C; } }

		public override string Mnemonic { get { return "INCB"; } }
	}
}
