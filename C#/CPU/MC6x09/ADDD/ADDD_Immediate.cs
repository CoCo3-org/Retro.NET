﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDD_Immediate : ADDD_Instruction, IImmediate 
	{
		public ADDD_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xC3; } }
	}
}