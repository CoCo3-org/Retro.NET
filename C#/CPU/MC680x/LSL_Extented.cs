using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LSL_Extended : Instruction, IExtended
	{
		public override byte OpCode { get { return 0x78; } }

		public override string Mnemonic { get { return "LSL"; } }
	}
}
