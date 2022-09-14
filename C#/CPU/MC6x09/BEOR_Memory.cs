using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BEOR_Memory : Instruction, IMemory 
	{
		public BEOR_Memory(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x34; } }

		public override string Mnemonic { get { return "BEOR"; } }
	}
}
