using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BITMD_Immediate : Instruction, IImmediate 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x3C; } }

		public override string Mnemonic { get { return "BITMD"; } }
	}
}
