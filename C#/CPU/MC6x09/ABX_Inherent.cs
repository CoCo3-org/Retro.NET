using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ABX_Inherent : Instruction, IInherent 
	{
		public ABX_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x3A; } }

		public override string Mnemonic { get { return "ABX"; } }
	}
}
