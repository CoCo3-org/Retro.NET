using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BGT_Relative : Instruction, IRelative 
	{
		public BGT_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x2E; } }

		public override string Mnemonic { get { return "BGT"; } }
	}
}
