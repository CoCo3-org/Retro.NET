using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DECA_Inherent : Instruction, IInherent
	{
		public override byte OpCode { get { return 0x4A; } }

		public override string Mnemonic { get { return "DECA"; } }
	}
}
