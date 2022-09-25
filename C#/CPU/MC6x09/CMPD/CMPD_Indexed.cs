﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CMPD_Indexed : CMPD_Instruction, IIndexed 
	{
		public CMPD_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xA3; } }
	}
}