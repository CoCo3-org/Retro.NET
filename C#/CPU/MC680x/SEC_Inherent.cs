using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SEC_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x0D; } }

		public override string Mnemonic { get { return "SEC"; } }
	}
}
