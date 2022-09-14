using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBEQ_Relative : Instruction, IRelative 
	{
		public LBEQ_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x27; } }

		public override string Mnemonic { get { return "LBEQ"; } }
	}
}
