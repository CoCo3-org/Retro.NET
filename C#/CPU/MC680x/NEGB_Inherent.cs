using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class NEGB_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x50; } }

		public override string Mnemonic { get { return "NEGB"; } }
	}
}
