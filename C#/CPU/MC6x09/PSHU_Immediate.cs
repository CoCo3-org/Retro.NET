using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class PSHU_Immediate : Instruction, IImmediate 
	{
		public PSHU_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x36; } }

		public override string Mnemonic { get { return "PSHU"; } }
	}
}
