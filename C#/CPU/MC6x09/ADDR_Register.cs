using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDR_Register : Instruction, IRegister 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x30; } }

		public override string Mnemonic { get { return "ADDR"; } }
	}
}
