using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TBA_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x17; } }

		public override string Mnemonic { get { return "TBA"; } }
	}
}
