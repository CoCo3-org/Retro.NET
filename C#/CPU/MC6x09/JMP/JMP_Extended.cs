using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class JMP_Extended : JMP_Instruction, IExtended 
	{
		public JMP_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x7E; } }
	}
}
