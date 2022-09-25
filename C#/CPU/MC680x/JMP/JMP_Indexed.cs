﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class JMP_Indexed : JMP_Instruction, IIndexed
	{
		public JMP_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x6E; } }
	}
}