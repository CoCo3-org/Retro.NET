using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORB_Extended : ORB_Instruction, IExtended 
	{
		public ORB_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xFA; } }
	}
}
