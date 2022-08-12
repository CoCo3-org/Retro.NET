using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDMD_Immediate : Instruction, IImmediate 
	{
		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x3D; } }

		public override string Mnemonic { get { return "LDMD"; } }
	}
}
