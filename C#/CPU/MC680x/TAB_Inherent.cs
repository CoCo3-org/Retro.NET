﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TAB_Inherent : Instruction, IInherent
	{
		public TAB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x16; } }

		public override string Mnemonic { get { return "TAB"; } }
	}
}