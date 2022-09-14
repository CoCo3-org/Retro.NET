using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class RORB_Inherent : Instruction, IInherent 
	{
		public RORB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x56; } }

		public override string Mnemonic { get { return "RORB"; } }
	}
}
