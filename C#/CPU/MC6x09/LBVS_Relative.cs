using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBVS_Relative : Instruction, IRelative 
	{
		public LBVS_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x29; } }

		public override string Mnemonic { get { return "LBVS"; } }
	}
}
