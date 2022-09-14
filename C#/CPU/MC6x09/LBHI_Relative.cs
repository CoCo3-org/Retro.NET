using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBHI_Relative : Instruction, IRelative 
	{
		public LBHI_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x22; } }

		public override string Mnemonic { get { return "LBHI"; } }
	}
}
