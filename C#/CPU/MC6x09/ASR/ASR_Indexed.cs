using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ASR_Indexed : ASR_Instruction, IIndexed 
	{
		public ASR_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x67; } }
	}
}
