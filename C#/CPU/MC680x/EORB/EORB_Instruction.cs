using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class EORB_Instruction : Instruction
	{
		public override string Mnemonic { get { return "EORB"; } }
	}
}
