using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LEAY_Indexed : Instruction, IIndexed 
	{
		public override byte OpCode { get { return 0x31; } }

		public override string Mnemonic { get { return "LEAY"; } }
	}
}
