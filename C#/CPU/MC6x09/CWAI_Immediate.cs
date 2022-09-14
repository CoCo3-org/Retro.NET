using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CWAI_Immediate : Instruction, IImmediate 
	{
		public CWAI_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x3C; } }

		public override string Mnemonic { get { return "CWAI"; } }
	}
}
