﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class INX_Inherent : Instruction, IInherent
	{
		public INX_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x08; } }

		public override string Mnemonic { get { return "INX"; } }
	}
}