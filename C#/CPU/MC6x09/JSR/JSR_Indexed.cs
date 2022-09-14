using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class JSR_Indexed : JSR_Instruction, IIndexed 
	{
		public JSR_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xAD; } }
	}
}
