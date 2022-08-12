using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class PSHX_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x3C; } }

		public override string Mnemonic { get { return "PSHX"; } }
	}
}
