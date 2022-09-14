using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDCC_Immediate : Instruction, IImmediate 
	{
		public ANDCC_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x1C; } }

		public override string Mnemonic { get { return "ANDCC"; } }
	}
}
