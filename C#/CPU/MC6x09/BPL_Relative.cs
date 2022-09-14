using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BPL_Relative : Instruction, IRelative 
	{
		public BPL_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x2A; } }

		public override string Mnemonic { get { return "BPL"; } }
	}
}
