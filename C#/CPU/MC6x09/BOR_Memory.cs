using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BOR_Memory : Instruction, IMemory 
	{
		public BOR_Memory(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x32; } }

		public override string Mnemonic { get { return "BOR"; } }
	}
}
