using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EXG_Immediate : Instruction, IImmediate 
	{
		public EXG_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x1E; } }

		public override string Mnemonic { get { return "EXG"; } }
	}
}
