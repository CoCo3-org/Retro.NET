using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STA_Indexed : STA_Instruction, IIndexed 
	{
		public STA_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xA7; } }
	}
}
