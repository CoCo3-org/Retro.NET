using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class NEG_Instruction : Instruction 
	{
		public override string Mnemonic { get { return "NEG"; } }
	}
}
