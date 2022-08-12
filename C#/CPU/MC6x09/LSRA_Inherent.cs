using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSRA_Inherent : Instruction, IInherent 
	{
		public override byte OpCode { get { return 0x44; } }

		public override string Mnemonic { get { return "LSRA"; } }
	}
}
