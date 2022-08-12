using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DAA_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x19; } }

		public override string Mnemonic { get { return "DAA"; } }
	}
}
