using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SWI_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x3F; } }

		public override string Mnemonic { get { return "SWI"; } }
	}
}
