﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EIM_Instruction : Instruction 
	{
		public EIM_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "EIM"; } }
	}
}