using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORR_Register : Instruction, IRegister 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x36; } }

		public override string Mnemonic { get { return "EORR"; } }
	}
}
