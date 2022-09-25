﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDW_Extended : LDW_Instruction, IExtended 
	{
		public LDW_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xB6; } }
	}
}