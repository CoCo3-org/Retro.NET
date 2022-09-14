using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EIM_Extended : EIM_Instruction, IExtended 
	{
		public EIM_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x75; } }
	}
}
