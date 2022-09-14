using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBGT_Relative : Instruction, IRelative 
	{
		public LBGT_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x2E; } }

		public override string Mnemonic { get { return "LBGT"; } }
	}
}
