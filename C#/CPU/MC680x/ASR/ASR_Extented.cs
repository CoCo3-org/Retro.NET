using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASR_Extended : ASR_Instruction, IExtended
	{
		public ASR_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x77; } }
	}
}
