using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class NEGA_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x40; } }

		public override string Mnemonic { get { return "NEGA"; } }
	}
}
