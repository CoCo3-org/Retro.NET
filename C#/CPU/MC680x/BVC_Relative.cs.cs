﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BVC_Relative : Instruction, IRelative
	{
		public BVC_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x28; } }

		public override string Mnemonic { get { return "BVC"; } }
	}
}