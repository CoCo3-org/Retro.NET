﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BLT_Relative : Instruction, IRelative
	{
		public BLT_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x2D; } }

		public override string Mnemonic { get { return "BLT"; } }
	}
}